using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DialoguePlayer : MonoBehaviour
{
    #region Variable
    [Header("UI")]
    public SpriteRenderer speakerSpriteRenederer;

    public TextMeshProUGUI dialogueNameText;
    public TextMeshProUGUI dialogueText;

    [Header("Infomation")]
    public bool isPlaying = false;

    public int curIndex;
    public int maxIndex;

    private List<Dialogue> playingDialogue;
    #endregion

    #region Function

    private void Start()
    {
        StartCoroutine(FadeOut(1, speakerSpriteRenederer));
        StartDialogue(DialogueManager.instance.dialogues);
    }

    public void StartDialogue(List<Dialogue> dialogues)
    {
        isPlaying = true;

        curIndex = 0;
        maxIndex = dialogues.Count;

        playingDialogue = dialogues;

        StartCoroutine(PlayDialogue(0));
    }

    public void EndDialogue()
    {
        isPlaying = false;
    }
    private IEnumerator PlayDialogue(int index)
    {


        Dialogue dialogue = playingDialogue[index];

        bool isNext = false;
        bool isTyping = dialogue.isTyping;
        bool haveCallBack = dialogue.haveCallBack;

        dialogueNameText.text = dialogue.speakerName;
        dialogueText.text = "";

        if(haveCallBack) dialogue.dialogueCallBack.OnStart.Invoke();

        if (index + 1 < playingDialogue.Count) isNext = true;
        else isNext = false;

        if (dialogue.speakerSprite != null) speakerSpriteRenederer.sprite = dialogue.speakerSprite;

        if (isTyping)
        {
            
            char[] text = dialogue.speakerText.ToCharArray();
            WaitForSeconds seconds = new WaitForSeconds(dialogue.typingSpeed);
            foreach (var item in text)
            {
                dialogueText.text += item;
                yield return seconds;
            }
        }
        else dialogueText.text = dialogue.speakerText;

        if (isNext)
        {
            while (true)
            {
                if (Input.GetMouseButton(0))
                {
                    StartCoroutine(PlayDialogue(index + 1));
                    break;
                }
                yield return null;
            }
            if(haveCallBack) dialogue.dialogueCallBack.OnEnd.Invoke();
        }
        else
        {
            if (haveCallBack) dialogue.dialogueCallBack.OnEnd.Invoke();
        }
    }

    private IEnumerator FadeIn(float speed, SpriteRenderer spriteRenderer)
    {
        float value = 0.3f;
        Color color = new Color(value, value, value, 1);
        while (value < 1)
        {
            value += Time.deltaTime * speed;
            color = new Color(value, value, value, 1);
            spriteRenderer.color = color;

            yield return null;
        }
    }
    private IEnumerator FadeOut(float speed, SpriteRenderer spriteRenderer)
    {
        float value = 1f;
        Color color = new Color(value, value, value, 1);
        while (value > 0.3f)
        {
            value -= Time.deltaTime * speed;
            color = new Color(value, value, value, 1);
            spriteRenderer.color = color;

            yield return null;
        }
    }
    #endregion
}
