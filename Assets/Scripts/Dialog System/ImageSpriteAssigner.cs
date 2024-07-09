using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ImageSpriteAssigner : MonoBehaviour
{
    [SerializeField] SpriteRenderer spriteRenderer;
    [SerializeField] Image image;

    void Update()
    {
        image.sprite = spriteRenderer.sprite;
    }
}
