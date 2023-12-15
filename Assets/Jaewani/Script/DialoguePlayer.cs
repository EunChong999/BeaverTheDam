using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialoguePlayer : MonoBehaviour
{
    [Header("UI")]
    public SpriteRenderer speakerSpriteRenederer;

    [Header("Infomation")]
    public bool isPlaying = false;

    public int curIndex;
    public int maxIndex;

    private List<Dialogue> playingDialogue;

    private void Start()
    {
        StartCoroutine(FadeOut(1, speakerSpriteRenederer));
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

        if (dialogue.speakerSprite != null) Debug.Log("юс╫ц");

        yield return null;
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
}
