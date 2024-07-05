using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ItemText : MonoBehaviour
{
    [SerializeField] SpriteRenderer spriteRenderer;
    [SerializeField] TextMeshPro text;

    // Update is called once per frame
    void Update()
    {
        text.alpha = spriteRenderer.color.a;
    }
}
