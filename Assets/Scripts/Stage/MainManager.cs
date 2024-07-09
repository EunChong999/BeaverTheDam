using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System;
using TMPro;

public enum MapType
{
    timeType,
    countType
}

public class MainManager : Manager
{
    public MapData curStage;
    [SerializeField] TextMeshProUGUI limitTypeText;
    [SerializeField] GameObject clearUI;
    [SerializeField] GameObject failUI;
    [SerializeField] Transform endCard;
    [SerializeField] Transform[] star;
    [SerializeField] GameObject nextStage;
    [SerializeField] UICurveCount UICurve;
    public int clearScore;
    public int integratedCount;
    public int StageIndex;
    [HideInInspector] public bool isEnd;

    [Serializable]
    public struct MapData
    {
        public int entireLimitTime;
        public int entireCount;
        public MapType type;
        public GameObject map;
        public int stars;
        public int timeLimit1;
        public int timeLimit2;
        public int countLimit1;
        public int countLimit2;
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
            limitTypeText.text = "TIME LIMIT";
        }
        
        if (curStage.type == MapType.countType)
        {
            integratedCount = curStage.entireCount;
            limitTypeText.text = "COUNT LIMIT";
        }

        UICurve.SetCurveCount(integratedCount);
    }

    public void AddRotateCount() => integratedCount++;
    public void MinusCurveCount()
    {
        integratedCount--;
        UICurve.SetCurveCount(integratedCount);
        if(integratedCount <= 0)
        {
            clearScore = 0;
            End(false);
        }
    }
    IEnumerator MinusTime()
    {
        while (true)
        {
            yield return new WaitForSeconds(1);
            integratedCount--;
            UICurve.SetCurveCount(integratedCount);
            if (integratedCount <= 0)
            {
                clearScore = 0;
                End(false);
                yield break;
            }
        }
    }
    public void Cancel()
    {
        StartCoroutine(CancelAnim());
    }
    IEnumerator CancelAnim()
    {
        if (SceneAnim.instance.canAnim)
        {
            SceneAnim.instance.AnimOn();
            yield return new WaitForSeconds(0.5f);
            SceneLoad(Scenes.SelectScene);
        }
    }
    public void StartStage(bool isRetry)
    {
        StartCoroutine(InitStage(isRetry));
    }

    IEnumerator InitStage(bool isRetry)
    {
        if (SceneAnim.instance.canAnim)
        {
            SceneAnim.instance.AnimOn();
            yield return new WaitForSeconds(0.5f);
            if (!isRetry) PlayerPrefs.SetInt("SelectIndex", StageIndex + 1);
            SceneLoad(Scenes.MainScene);
        }
    }

    public void End(bool isCleared)
    {
        isEnd = true;

        StartCoroutine(EndMove());

        var clearindex = 0;

        if (isCleared)
        {
            clearindex = StageIndex + 1;

            clearUI.SetActive(true);
            failUI.SetActive(false);
        }
        else
        {
            clearUI.SetActive(false);
            failUI.SetActive(true);
        }

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
