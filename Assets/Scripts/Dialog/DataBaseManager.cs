using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

[System.Serializable]
public class StageData
{
    public float[] StarTimer;
}
public class DataBaseManager : MonoBehaviour
{
    public static DataBaseManager instance { get; private set; }

    public string csv_File;

    Dictionary<int, Dialogue> dialogueDic = new Dictionary<int, Dialogue>();

    public static bool isFinish = false;

    public readonly string ADDRESS = "https://docs.google.com/spreadsheets/d/1BbJ6btodZXZCIMzK0Sj2fd0S1ngLJ9iXDCivDr7C6BQ";
    public readonly string RANGE = "A2:E";
    public readonly long[] SHEET_ID = { 1042825153, 1383170883};

    public StageData[] stageData;

    private void Awake()
    {
        StartCoroutine(DialogLoadData());
    }
    private void Start()
    {

        if (instance == null) instance = this;
    }
    public Dialogue[] GetDialogues(int _StartNum, int _EndNum)
    {
        List<Dialogue> dialogueList = new List<Dialogue>();

        for (int i = 0; i <= _EndNum - _StartNum; i++)
        {
            dialogueList.Add(dialogueDic[_StartNum + i]);
        }

        return dialogueList.ToArray();
    }
    private IEnumerator DialogLoadData()
    {
        UnityWebRequest www = UnityWebRequest.Get(DialogueEvent.GetCSVAddress(ADDRESS, RANGE, SHEET_ID[0]));
        yield return www.SendWebRequest();

        csv_File = www.downloadHandler.text;
        CSVParser theParser = GetComponent<CSVParser>();

        Dialogue[] dialogues = theParser.Parse(csv_File);
        for (int i = 0; i < dialogues.Length; i++)
        {
            dialogueDic.Add(i + 1, dialogues[i]);
        }
        StartCoroutine(StageLoadData());
    }
    private IEnumerator StageLoadData()
    {
        UnityWebRequest www = UnityWebRequest.Get(DialogueEvent.GetCSVAddress(ADDRESS, RANGE, SHEET_ID[1]));
        yield return www.SendWebRequest();

        var data = www.downloadHandler.text;
        var column = data.Split("\n");
        stageData = new StageData[column.Length];
        for(int i = 0; i < column.Length; i++)
        {
            stageData[i] = new StageData();
            var row = column[i].Split(",");
            stageData[i].StarTimer = new float[row.Length - 1];
            for(int j = 1; j < row.Length; j++)
            {
                stageData[i].StarTimer[j - 1] = float.Parse(row[j]);
            }
        }

        isFinish = true;
    }
}
