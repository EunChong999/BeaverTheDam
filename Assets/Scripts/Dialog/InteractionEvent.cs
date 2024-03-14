using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionEvent : MonoBehaviour
{
    [SerializeField] DialogueEvent dialogue;

    public Dialogue[] GetDialogue()
    {
        dialogue.dialogues 
        = DataBaseManager.instance.GetDialogues((int)dialogue.line.x,(int)dialogue.line.y);
        return dialogue.dialogues;
    }
}
