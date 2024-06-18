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
    private void Start()
    {
        if(instance != null) Destroy(gameObject);
        instance = this;
        DontDestroyOnLoad(transform.parent);

        sp = GetComponent<Image>();
    }
    public void AnimOn(bool isClose)
    {
        if(isClose)
        StartCoroutine(AnimClose());
        else
        StartCoroutine(AnimOpen());
    }
    IEnumerator AnimClose()
    {
        for(int i = sprites.Length - 1; i > 0; i--)
        {
            sp.sprite = sprites[i];
            var a = Mathf.InverseLerp(sprites.Length - 1, 0, i);
            print(a);
            sp.color = new Color(1,1,1,a);
            yield return new WaitForSeconds(0.02f);
        }
    }
    IEnumerator AnimOpen()
    {
        for(int i = 0; i < sprites.Length; i++)
        {
            sp.sprite = sprites[i];
            var a = Mathf.InverseLerp(sprites.Length - 1, 0, i);
            sp.color = new Color(1,1,1,a);
            yield return new WaitForSeconds(0.01f);
        }
    }
}
