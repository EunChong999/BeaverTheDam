using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchToStart : Manager
{
    RectTransform touchText;
    [SerializeField] float range = 1;
    [SerializeField] float speed = 1;
    Vector2 curPos;
    private void Start()
    {
        touchText = GetComponent<RectTransform>();
        touchText.anchoredPosition = GetComponent<UIInitializer>().pos;
        curPos = touchText.anchoredPosition;
    }
    private void Update()
    {
        touchText.anchoredPosition = curPos + new Vector2(0, Mathf.Sin(Time.time * speed) * range);
        if (Input.GetMouseButtonDown(0))
        {
            if (SceneAnim.instance.canAnim)
            {
                SceneAnim.instance.AnimOn();
                StartCoroutine(SceneMove());
            }
        }
    }
    IEnumerator SceneMove()
    {
        yield return new WaitForSeconds(0.5f);
        SceneMove(Scenes.SelectScene);
    }
}
