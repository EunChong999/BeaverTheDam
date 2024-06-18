using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SceneAnim : MonoBehaviour
{
    public static SceneAnim instance {get; private set;}
    [SerializeField] Sprite[] sprites;
    Image sp;
    public bool canAnim = true;

    private void Awake()
    {
        if (instance != null) Destroy(gameObject);
        else
            while (instance == null) 
                instance = this;

        DontDestroyOnLoad(transform.parent);

        sp = GetComponent<Image>();
    }
    public void AnimOn()
    {
        if (canAnim)
        {
            StartCoroutine(AnimClose());
            canAnim = false;
        }
    }
    IEnumerator AnimClose()
    {
        sp.enabled = true;

        for (int i = sprites.Length - 1; i > 0; i--)
        { 
            sp.sprite = sprites[i];
            var a = Mathf.InverseLerp(sprites.Length - 1, 0, i);
            sp.color = new Color(1,1,1,a);
            yield return new WaitForSeconds(0.01f);
        }

        yield return new WaitForSeconds(0.5f);

        StartCoroutine(AnimOpen());
    }
    IEnumerator AnimOpen()
    {
        for (int i = 0; i < sprites.Length; i++)
        {
            sp.sprite = sprites[i];
            var a = Mathf.InverseLerp(sprites.Length - 1, 0, i);
            sp.color = new Color(1,1,1,a);
            yield return new WaitForSeconds(0.01f);
        }

        canAnim = true;    
    }
}
