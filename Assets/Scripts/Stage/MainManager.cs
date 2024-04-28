using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class MainManager : Manager
{
    [SerializeField] Transform endCard;
    [SerializeField] Transform[] star;
    [SerializeField] GameObject nextStage;
    public int clearScore;
    public int rotateCount;
    public int StageIndex;
    public static MainManager instance {get; private set;}
    private void Awake()
    {
        instance = this;
    }
    private void Start()
    {
        StageIndex = PlayerPrefs.GetInt("SelectIndex");
        print(StageIndex);
        endCard.localScale = Vector3.zero;
        nextStage.SetActive(StageIndex < PlayerPrefs.GetInt("MaxIndex"));
        for (int i = 0; i < star.Length; i++)
        {
            star[i].localScale = Vector3.zero;
        }
    }
    public void AddRotateCount() => rotateCount++;
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
