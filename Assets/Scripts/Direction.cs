using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Direction : MonoBehaviour
{
    [SerializeField] BasicBuilding basicBuilding;
    [SerializeField] Sprite[] sprites;
    [SerializeField] Transform spriteTransform;
    
    SpriteRenderer spriteRenderer;

    moveType moveType;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        moveType = basicBuilding.moveType;
    }

    void Update()
    {
        transform.localRotation = spriteTransform.localRotation;

        switch (moveType)
        {
            case moveType.straightType:
                if (transform.parent.localRotation.eulerAngles.y >= 0 && transform.parent.localRotation.eulerAngles.y < 90)
                {
                    spriteRenderer.sprite = sprites[1];
                }
                else if (transform.parent.localRotation.eulerAngles.y >= 90 && transform.parent.localRotation.eulerAngles.y < 180)
                {
                    spriteRenderer.sprite = sprites[5];
                }
                else if (transform.parent.localRotation.eulerAngles.y >= 180 && transform.parent.localRotation.eulerAngles.y < 270)
                {
                    spriteRenderer.sprite = sprites[4];
                }
                else if (transform.parent.localRotation.eulerAngles.y >= 270 && transform.parent.localRotation.eulerAngles.y < 360)
                {
                    spriteRenderer.sprite = sprites[0];
                }
                break;
            case moveType.curveType:
                if (transform.parent.localRotation.eulerAngles.y >= 0 && transform.parent.localRotation.eulerAngles.y < 90)
                {
                    spriteRenderer.sprite = sprites[2];
                }
                else if (transform.parent.localRotation.eulerAngles.y >= 90 && transform.parent.localRotation.eulerAngles.y < 180)
                {
                    spriteRenderer.sprite = sprites[5];
                }
                else if (transform.parent.localRotation.eulerAngles.y >= 180 && transform.parent.localRotation.eulerAngles.y < 270)
                {
                    spriteRenderer.sprite = sprites[6];
                }
                else if (transform.parent.localRotation.eulerAngles.y >= 270 && transform.parent.localRotation.eulerAngles.y < 360)
                {
                    spriteRenderer.sprite = sprites[7];
                }
                break;
        }

        if (transform.parent.localRotation.eulerAngles.y >= 0 && transform.parent.localRotation.eulerAngles.y < 90)
        {
            transform.localPosition = new Vector3(-0.1f, 1.25f, -0.125f);
        }
        else if (transform.parent.localRotation.eulerAngles.y >= 90 && transform.parent.localRotation.eulerAngles.y < 180)
        {
            transform.localPosition = new Vector3(0.1f, 1.25f, -0.125f);
        }
        else if (transform.parent.localRotation.eulerAngles.y >= 180 && transform.parent.localRotation.eulerAngles.y < 270)
        {
            transform.localPosition = new Vector3(0.1f, 1.25f, 0.125f);
        }
        else if (transform.parent.localRotation.eulerAngles.y >= 270 && transform.parent.localRotation.eulerAngles.y < 360)
        {
            transform.localPosition = new Vector3(-0.1f, 1.25f, 0.125f);
        }
    }
}
