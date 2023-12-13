using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutomaticSortLayer : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void LateUpdate()
    {
        spriteRenderer.sortingOrder = (int)(transform.parent.position.y);
    }
}
