using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class InteractionEvent : MonoBehaviour
{
    [SerializeField] DialogueEvent dialogue;
    public readonly string ADDRESS = "https://docs.google.com/spreadsheets/d/1BbJ6btodZXZCIMzK0Sj2fd0S1ngLJ9iXDCivDr7C6BQ";
    public readonly string RANGE = "A2:C";
    public readonly long SHEET_ID = 1042825153;
    private void Start() {
        StartCoroutine(LoadData());
    }
    private IEnumerator LoadData()
    {
        UnityWebRequest www = UnityWebRequest.Get(DialogueEvent.GetTSVAddress(ADDRESS,RANGE,SHEET_ID));
        yield return www.SendWebRequest();

        Debug.Log(www.downloadHandler.text);
    }
}
