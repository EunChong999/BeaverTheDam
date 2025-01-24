using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System;
using TMPro;
using System.IO;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;

public enum MapType
{
    time,
    count
}

public class MainManager : Manager
{
    public AudioSource test;
    public MapData curStage;
    private StageDB stageDB;
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
        public GameObject map;
        public string branch;
        public MapType type;
        public int entireLimitAmount;
        public int firstTimeLimit;
        public int secondTimeLimit;
        public int firstCountLimit;
        public int secondCountLimit;
        public int stars;
        public bool isCleared;
    }
    
    public MapData[] Maps;
    public static MainManager instance {get; private set;}
    private void Awake()
    {
        SetUp();
    }
    private void Start()
    {
        for (int i = 0; i < Maps.Length; i++)
        {
            Maps[i].isCleared = stageDB.Entites[i].clear; 
        }

        StageIndex = PlayerPrefs.GetInt("SelectIndex");
        endCard.localScale = Vector3.zero;
        nextStage.SetActive(StageIndex < PlayerPrefs.GetInt("MaxIndex"));
        for (int i = 0; i < star.Length; i++)
        {
            star[i].localScale = Vector3.zero;
        }

        curStage = Maps[StageIndex];
        integratedCount = curStage.entireLimitAmount;
        UICurve.SetCurveCount(integratedCount);

        Instantiate(curStage.map).SetActive(true);

        if (curStage.type == MapType.time)
        {
            StartCoroutine(MinusTime());
            limitTypeText.text = "TIME LIMIT";
        }
        
        if (curStage.type == MapType.count)
        {
            limitTypeText.text = "COUNT LIMIT";
        }

        UICurve.SetCurveCount(integratedCount);
    }

    public void SetUp()
    {
        instance = this;

        stageDB = Resources.Load<StageDB>("StageDB");
        
        int index = 0;
        for (int i = 0; i < Maps.Length; i++)
        {
            if (stageDB.Entites[i].branch == Maps[index].branch)
            {
                Maps[index].type = (MapType)Enum.Parse(typeof(MapType),stageDB.Entites[i].limit_type);
                Maps[index].entireLimitAmount = Convert.ToInt32(stageDB.Entites[i].limit_amount);
                Maps[index].firstTimeLimit = Convert.ToInt32(stageDB.Entites[i].first_limit);
                Maps[index].firstCountLimit = Convert.ToInt32(stageDB.Entites[i].first_limit);
                Maps[index].secondTimeLimit = Convert.ToInt32(stageDB.Entites[i].second_limit);
                Maps[index].secondCountLimit = Convert.ToInt32(stageDB.Entites[i].second_limit);
                Maps[index].stars = Convert.ToInt32(stageDB.Entites[i].stars);
                index++;
            }
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
            End(false);
        }
    }
    IEnumerator MinusTime()
    {
        while (true)
        {
            while (DialogSystem.instance != null && !DialogSystem.instance.isDialogSystemEnded)
                yield return null;

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

        Time.timeScale = 1;

        int clearIndex = 0;

        if ((isCleared || Maps[StageIndex].isCleared) && StageIndex < Maps.Length - 1)
        {
            clearIndex = StageIndex + 1;
            stageDB.Entites[StageIndex + 1].clear = true;

            clearUI.SetActive(true);
            failUI.SetActive(false);
        }
        else
        {
            clearUI.SetActive(false);
            failUI.SetActive(true);
        }

        if (clearIndex > PlayerPrefs.GetInt("CanSelectIndex") && StageIndex < PlayerPrefs.GetInt("MaxIndex"))
            PlayerPrefs.SetInt("CanSelectIndex", clearIndex);

        StartCoroutine(EndMove());
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
