using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class MainManager : Manager
{
    [SerializeField] Transform endCard;
    [SerializeField] Transform[] star;
    public int clearValue;
    private void Start() {
        endCard.localScale = Vector3.zero;
        for(int i = 0; i < star.Length; i++)
        {
            star[i].localScale = Vector3.zero;
        }
    }
    public void Cancel()
    {
        SceneMove(Scenes.SelectScene);
    }
    public void ReStage(bool isRetry)
    {
        //if(!isRetry) stageIndex++;
        SceneMove(Scenes.MainScene);
    }
    public void End()
    {
        StartCoroutine(EndMove());
    }
    IEnumerator EndMove()
    {
        yield return endCard.DOScale(Vector3.one,0.5f).SetEase(Ease.OutBack).WaitForCompletion();
        for(int i = 0; i < clearValue; i++)
        {
            yield return star[i].DOScale(Vector3.one,0.5f).SetEase(Ease.OutBack).WaitForCompletion();
        }
    }
}
