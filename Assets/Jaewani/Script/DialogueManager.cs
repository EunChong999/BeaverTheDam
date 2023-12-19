using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class Dialogue
{
    private DialogueCallBack dialogueCallBack;

    [HideInInspector]
    public bool haveCallBack = false;

    public string speakerText;
    public string speakerName;

    public Sprite speakerSprite;

    public float typingSpeed;

    public void InitCallBack()
    {
        haveCallBack = true;
        var callBackObject = new GameObject().AddComponent<DialogueCallBack>();
        callBackObject.transform.SetParent(DialogueManager.instance.transform);
        callBackObject.name = speakerText;
        this.dialogueCallBack = callBackObject;
    }
    public void AddOnStart(Action action)
    {
        if (haveCallBack) dialogueCallBack.AddOnStart(action);
        else Debug.LogError("Dialogue Call Back�� ��ϵ��� �ʾҽ��ϴ�. \n 'InitCallBack' �Լ��� ���� ȣ�����ּ���.");
    }
}

public class DialogueManager : MonoBehaviour
{
    public static DialogueManager instance;

    public List<Dialogue> dialogues = new List<Dialogue>();
    private void Awake()
    {
        if (instance == null) instance = this;
        else Destroy(gameObject);
    }
    void Start()
    {
        
    }

    void Update()
    {

    }
   
}
