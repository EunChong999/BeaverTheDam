using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    [SerializeField] GameObject DialogueObj;
    [SerializeField] Text nameUI;
    [SerializeField] Text contextUI;

    [SerializeField] Dialogue[] curDialogue;
    public InteractionEvent dialogues;
    public float textSpeed;
    int curDialogueIndex;
    int curContextIndex;

    public void StartDialogue()
    {
        DialogueObj.SetActive(true);
        curDialogue = dialogues.GetDialogue();
        curDialogueIndex = 0;
        curContextIndex = 0;
        InitDialogue();
    }
    public void InitDialogue()
    {
        nameUI.text = curDialogue[curDialogueIndex].name;
        StartCoroutine(TypingText(curDialogue[curDialogueIndex].contexts[curContextIndex]));
    }
    IEnumerator TypingText(string text)
    {
        var wait = new WaitForSeconds(textSpeed);
        int count = 0;
        while(text.Length >= count)
        {
            contextUI.text = text.Substring(0,count);
            count++;
            yield return wait;
        }
    }
    public void NextDialogue()
    {
        if(curContextIndex < curDialogue[curDialogueIndex].contexts.Length - 1) curContextIndex++;
        else
        {
            curContextIndex = 0;
            if(curDialogueIndex < curDialogue.Length - 1) curDialogueIndex++;
                else EndDialogue();
        }
        InitDialogue();
    }
    public void EndDialogue()
    {
        DialogueObj.SetActive(false);
    }
}
