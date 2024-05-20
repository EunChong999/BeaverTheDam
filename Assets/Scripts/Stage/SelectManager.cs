using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class StageBtn
{
    public Button[] button;
}
public class SelectManager : Manager
{
    [SerializeField] StageBtn[] buttons;
    [SerializeField] Transform btnTransform;
    [SerializeField] Transform terrainTransform;
    [SerializeField] Transform chapterTextBtnTransform;
    [SerializeField] Button[] chapterBtn;
    [SerializeField] Button[] chapterTextBtn;
    public int clearIndex;
    public int maxChapter;

    int curChapter;
    private void Start()
    {
        clearIndex = PlayerPrefs.GetInt("CanSelectIndex");
        curChapter = PlayerPrefs.GetInt("curChapter");
        maxChapter = buttons.Length - 1;
        print(clearIndex);
        InitStageButton();
        btnTransform.DOLocalMoveX(curChapter * -1920, 0);
        ChapterMove(0);
    }
    public void ClearReset()
    {
        PlayerPrefs.SetInt("CanSelectIndex",0);
        SceneMove(Scenes.SelectScene);
    }
    public void InitStageButton()
    {
        var maxIndex = 0;
        for (int i = 0; i < buttons.Length; i++)
        {
            for (int j = 0; j < buttons[i].button.Length; j++)
            {
                var numj = j;
                var numi = i;
                var max = maxIndex;
                bool canSelect = numj + max <= clearIndex;
                buttons[numi].button[numj].onClick.AddListener(() =>
                {
                    if (canSelect)
                    {
                        PlayerPrefs.SetInt("SelectIndex", numj + max);
                        PlayerPrefs.SetInt("curChapter", curChapter);
                        StageStart();
                    }
                });
                buttons[numi].button[numj].image.color = canSelect ? new Color(1, 1, 1, 1) : new Color(0.5f, 0.5f, 0.5f, 1);
            }
            maxIndex += buttons[i].button.Length;
        }
        PlayerPrefs.SetInt("MaxIndex", maxIndex);

        for(int i = 0; i < chapterTextBtn.Length; i++)
        {
            var num = i;
            chapterTextBtn[num].onClick.AddListener(() => ChapterMove(num - curChapter));
        }
    }
    public void StageStart()
    {
        SceneMove(Scenes.MainScene);
    }
    public void ChapterMove(int addIndex)
    {
        curChapter += addIndex;
        chapterBtn[0].gameObject.SetActive(curChapter > 0);
        chapterBtn[1].gameObject.SetActive(curChapter < maxChapter);
        btnTransform.DOLocalMove(new Vector2(curChapter * -1920,(curChapter * -1120) + 90), 0.5f).SetEase(Ease.InOutQuad);
        terrainTransform.DOLocalMove(new Vector2(curChapter * -3,(curChapter *-1.75f) + 0.15f), 0.5f).SetEase(Ease.InOutQuad);
        for(int i = 0; i < chapterTextBtn.Length; i++)
        {
            var text = chapterTextBtn[i].transform.GetChild(0).GetComponent<Text>();
            text.color = i == curChapter ? Color.white : Color.grey;
        }
        var pos = new Vector2(curChapter * -150,(curChapter * -75) -200);
        print(pos);
        chapterTextBtnTransform.DOLocalMove(pos,0.5f);
    }
}
