using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Dialogue
{
    [Tooltip("캐릭터 이름")]
    public string name;

    [Tooltip("대사")]
    public string[] contexts;
    [Tooltip("캐릭터 ID")]
    public string CharID;
    [Tooltip("위치")]
    public Vector2 pos;
}

[System.Serializable]
public class DialogueEvent
{
    public string name;

    public Vector2 line;
    public Dialogue[] dialogues;

    public static string GetCSVAddress(string address, string range, long sheetID)
    {
        return $"{address}/export?format=csv&range={range}&gid={sheetID}";
    }
}
