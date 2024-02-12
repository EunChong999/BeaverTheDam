using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectManager : Manager
{
    [SerializeField] Button[] Stagebutton;
    public int clearIndex;
    private void Start() {
        //PlayerPrefs.SetInt("CanSelectIndex",0);
        clearIndex = PlayerPrefs.GetInt("CanSelectIndex");
        PlayerPrefs.SetInt("MaxIndex",Stagebutton.Length - 1);
        print(clearIndex);
        for(int i = 0; i < Stagebutton.Length; i++)
        {
            var num = i;
            bool canSelect = num <= clearIndex;
            Stagebutton[num].onClick.AddListener(() => 
            {
                if(canSelect)
                {
                    PlayerPrefs.SetInt("SelectIndex", num);
                    StageStart();
                }
            });
            Stagebutton[num].image.color = canSelect ? new Color(1,1,1,1) : new Color(0.5f,0.5f,0.5f,1);
        }
    }
    public void StageStart()
    {
        SceneMove(Scenes.MainScene);
    }
}
