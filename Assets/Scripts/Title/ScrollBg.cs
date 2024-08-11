using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollBg : MonoBehaviour
{
    [SerializeField] float speed;
    RectTransform rect;
    void Start()
    {
        rect = GetComponent<RectTransform>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(speed * Time.deltaTime * Vector2.left);
        if(rect.anchoredPosition.x < -rect.sizeDelta.x)
        {
            rect.anchoredPosition += 2 * Vector2.right * rect.sizeDelta.x;
        }
    }
}
