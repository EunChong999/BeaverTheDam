using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System;

public enum MapType
{
    timeType,
    countType
}

public class MainManager : Manager
{
    public MapData curStage;
    [SerializeField] Transform endCard;
    [SerializeField] Transform[] star;
    [SerializeField] GameObject nextStage;
    [SerializeField] UICurveCount UICurve;
    public int clearScore;
    public int integratedCount;
    public int StageIndex;

    [Serializable]
    public struct MapData
    {
        public int entireLimitTime;
        public int entireCount;
        public MapType type;
        public GameObject map;
        public int limitTime1;
        public int limitTime2;
        public int limitTime3;
        public int count1;
        public int count2;
        public int count3;
    }

    public MapData[] Maps;
    public static MainManager instance {get; private set;}
    private void Awake()
    {
        instance = this;
    }
    private void Start()
    {
        StageIndex = PlayerPrefs.GetInt("SelectIndex");
        endCard.localScale = Vector3.zero;
        nextStage.SetActive(StageIndex < PlayerPrefs.GetInt("MaxIndex"));
        for (int i = 0; i < star.Length; i++)
        {
            star[i].localScale = Vector3.zero;
        }

        curStage = Maps[StageIndex];
        integratedCount = curStage.entireCount;
        UICurve.SetCurveCount(integratedCount);

        Instantiate(curStage.map).SetActive(true);

        if (curStage.type == MapType.timeType)
        {
            integratedCount = curStage.entireLimitTime;
            StartCoroutine(MinusTime());
        }
        else if (curStage.type == MapType.countType)
        {
            integratedCount = curStage.entireCount;
        }
    }

    public void AddRotateCount() => integratedCount++;
    public void MinusCurveCount()
    {
        integratedCount--;
        UICurve.SetCurveCount(integratedCount);
        if(integratedCount <= 0)
        {
            clearScore = 0;
            End();
        }
    }
    IEnumerator MinusTime()
    {
        while (true)
        {
            yield return new WaitForSeconds(1);
            integratedCount--;
            if (integratedCount <= 0)
            {
                clearScore = 0;
                End();
                yield break;
            }
        }
    }
    public void Cancel()
    {
        SceneMove(Scenes.SelectScene);
    }
    public void StartStage(bool isRetry)
    {
        if (!isRetry) PlayerPrefs.SetInt("SelectIndex", StageIndex + 1);
        SceneMove(Scenes.MainScene);
    }
    public void End()
    {
        StartCoroutine(EndMove());
        var clearindex = StageIndex + 1;
        if (clearindex > PlayerPrefs.GetInt("CanSelectIndex") && StageIndex < PlayerPrefs.GetInt("MaxIndex"))
            PlayerPrefs.SetInt("CanSelectIndex", clearindex);
    }
    IEnumerator EndMove()
    {
        yield return endCard.DOScale(Vector3.one, 0.5f).SetEase(Ease.OutBack).WaitForCompletion();
        for (int i = 0; i < clearScore; i++)
        {
            yield return star[i].DOScale(Vector3.one, 0.5f).SetEase(Ease.OutBack).WaitForCompletion();
        }
    }
}
