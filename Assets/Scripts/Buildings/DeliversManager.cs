using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeliversManager : MonoBehaviour
{
    [SerializeField] 
    private Delivers[] delivers;
    bool isEnded;

    private void Update()
    {
        if (!isEnded)
            CheckAllDeliversAccept();
    }

    private void CheckAllDeliversAccept()
    {
        foreach (var deliver in delivers)
        {
            if (!deliver.isAllAccepted)
            {
                return;
            }
        }

        if (MainManager.instance.curStage.type == MapType.time)
        {
            if (MainManager.instance.integratedCount >= MainManager.instance.curStage.firstTimeLimit)
                MainManager.instance.clearScore = 3;
            else if (MainManager.instance.integratedCount >= MainManager.instance.curStage.secondTimeLimit)
                MainManager.instance.clearScore = 2;
            else
                MainManager.instance.clearScore = 1;
        }
        else if (MainManager.instance.curStage.type == MapType.count)
        {
            if (MainManager.instance.integratedCount >= MainManager.instance.curStage.firstCountLimit)
                MainManager.instance.clearScore = 3;
            else if (MainManager.instance.integratedCount >= MainManager.instance.curStage.secondCountLimit)
                MainManager.instance.clearScore = 2;
            else
                MainManager.instance.clearScore = 1;
        }

        MainManager.instance.End(true);

        isEnded = true;
    }
}
