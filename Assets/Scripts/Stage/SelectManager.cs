using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectManager : Manager
{
    public void StageStart()
    {
        SceneMove(Scenes.MainScene);
    }
}
