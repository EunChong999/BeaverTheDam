using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueCallBack : MonoBehaviour
{
    public Action OnStart;
    public Action OnEnd;

    public void AddOnStart(Action action) => OnStart += action;
    public void AddOnEnd(Action action) => OnEnd += action;
}
