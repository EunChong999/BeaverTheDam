using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    [SerializeField] GameObject DialogueObj;
    [SerializeField] Text nameUI;
    [SerializeField] Text contextUI;

    [SerializeField] Dialogue[] curDialogue;
    [SerializeField] Transform charTransform;
    [SerializeField] Image charPrefab;
    public Dictionary<string, Image> characterImages = new Dictionary<string, Image>();

    public InteractionEvent dialogues;
    public Coroutine dialogueAnim;
    public float textSpeed;
    int curDialogueIndex;
    int curContextIndex;
    bool isPlay;

    public void StartDialogue()
    {
        DialogueObj.SetActive(true);
        curDialogue = dialogues.GetDialogue();
        curDialogueIndex = 0;
        curContextIndex = 0;
        InitDialogue();
    }
    private void Update()
    {
        isPlay = dialogueAnim != null;
    }
    public void InitDialogue()
    {
        var curName = curDialogue[curDialogueIndex].name;
        nameUI.text = curName;

        Image curImage;
        if (characterImages.ContainsKey(curName))
        {
            curImage = characterImages[curName];
            curImage.gameObject.SetActive(true);
        }
        else
        {
            curImage = Instantiate(charPrefab, charTransform);
            characterImages.Add(curName, curImage);
        }
        curImage.rectTransform.anchoredPosition = Camera.main.WorldToScreenPoint(curDialogue[curDialogueIndex].pos);

        if (!isPlay) dialogueAnim = StartCoroutine(TypingText(curDialogue[curDialogueIndex].contexts[curContextIndex]));
        else contextUI.text = curDialogue[curDialogueIndex].contexts[curContextIndex];
    }
    IEnumerator TypingText(string text)
    {
        var wait = new WaitForSeconds(textSpeed);
        int count = 0;
        while (text.Length >= count)
        {
            contextUI.text = text.Substring(0, count);
            count++;
            yield return wait;
        }
        dialogueAnim = null;
    }
    public void NextDialogue()
    {
        if (curContextIndex < curDialogue[curDialogueIndex].contexts.Length - 1)
        {
            if (!isPlay) curContextIndex++;
            else
            {
                StopCoroutine(dialogueAnim);
                dialogueAnim = null;
            }
        }
        else
        {
            curContextIndex = 0;
            if (curDialogueIndex < curDialogue.Length - 1) curDialogueIndex++;
            else
            {
                EndDialogue();
                return;
            }
        }
        InitDialogue();
    }
    public void EndDialogue()
    {
        var charList = characterImages.Keys.ToList();
        DialogueObj.SetActive(false);
        for (int i = 0; i < charList.Count; i++)
        {
            characterImages[charList[i]].gameObject.SetActive(false);
        }
    }
}
