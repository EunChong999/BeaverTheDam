using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Direction : MonoBehaviour
{
    [SerializeField] BasicBuilding basicBuilding;
    [SerializeField] Sprite[] sprites;
    [SerializeField] Transform spriteTransform;
    [SerializeField] bool isFrontReversed;

    SpriteRenderer spriteRenderer;

    movementType movementType;
    directionType directionType;

    int rotationToInt;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        movementType = basicBuilding.movementType;
        spriteRenderer.color = BuildingManager.instance.directionColor;
        gameObject.SetActive(false);
    }

    void LateUpdate()
    {
        transform.localRotation = spriteTransform.localRotation;
        rotationToInt = Mathf.RoundToInt(transform.parent.rotation.eulerAngles.y);
        directionType = basicBuilding.directionType;

        if (movementType == movementType.straightType)
        {
            if (rotationToInt >= 0 && rotationToInt < 90)
            {
                spriteRenderer.sprite = sprites[1];
            }
            else if (rotationToInt >= 90 && rotationToInt < 180)
            {
                spriteRenderer.sprite = sprites[7];
            }
            else if (rotationToInt >= 180 && rotationToInt < 270)
            {
                spriteRenderer.sprite = sprites[6];
            }
            else if (rotationToInt >= 270 && rotationToInt < 360)
            {
                spriteRenderer.sprite = sprites[0];
            }
        }
        else
        {
            if (directionType == directionType.rightType)
            {
                if (isFrontReversed)
                {
                    if (rotationToInt >= 0 && rotationToInt < 90)
                    {
                        spriteRenderer.sprite = sprites[5];
                    }
                    else if (rotationToInt >= 90 && rotationToInt < 180)
                    {
                        spriteRenderer.sprite = sprites[8];
                    }
                    else if (rotationToInt >= 180 && rotationToInt < 270)
                    {
                        spriteRenderer.sprite = sprites[10];
                    }
                    else if (rotationToInt >= 270 && rotationToInt < 360)
                    {
                        spriteRenderer.sprite = sprites[3];
                    }
                }
                else
                {
                    if (rotationToInt >= 0 && rotationToInt < 90)
                    {
                        spriteRenderer.sprite = sprites[2];
                    }
                    else if (rotationToInt >= 90 && rotationToInt < 180)
                    {
                        spriteRenderer.sprite = sprites[11];
                    }
                    else if (rotationToInt >= 180 && rotationToInt < 270)
                    {
                        spriteRenderer.sprite = sprites[9];
                    }
                    else if (rotationToInt >= 270 && rotationToInt < 360)
                    {
                        spriteRenderer.sprite = sprites[4];
                    }
                }
            }
            else
            {
                if (isFrontReversed)
                {
                    if (rotationToInt >= 0 && rotationToInt < 90)
                    {
                        spriteRenderer.sprite = sprites[11];
                    }
                    else if (rotationToInt >= 90 && rotationToInt < 180)
                    {
                        spriteRenderer.sprite = sprites[9];
                    }
                    else if (rotationToInt >= 180 && rotationToInt < 270)
                    {
                        spriteRenderer.sprite = sprites[4];
                    }
                    else if (rotationToInt >= 270 && rotationToInt < 360)
                    {
                        spriteRenderer.sprite = sprites[2];
                    }
                }
                else
                {
                    if (rotationToInt >= 0 && rotationToInt < 90)
                    {
                        spriteRenderer.sprite = sprites[8];
                    }
                    else if (rotationToInt >= 90 && rotationToInt < 180)
                    {
                        spriteRenderer.sprite = sprites[10];
                    }
                    else if (rotationToInt >= 180 && rotationToInt < 270)
                    {
                        spriteRenderer.sprite = sprites[3];
                    }
                    else if (rotationToInt >= 270 && rotationToInt < 360)
                    {
                        spriteRenderer.sprite = sprites[5];
                    }
                }
            }
        }

        if (rotationToInt >= 0 && rotationToInt < 90)
        {
            transform.localPosition = new Vector3(-0.2f, 1.25f, -0.25f);
        }
        else if (rotationToInt >= 90 && rotationToInt < 180)
        {
            transform.localPosition = new Vector3(0.2f, 1.25f, -0.25f);
        }
        else if (rotationToInt >= 180 && rotationToInt < 270)
        {
            transform.localPosition = new Vector3(0.2f, 1.25f, 0.25f);
        }
        else if (rotationToInt >= 270 && rotationToInt < 360)
        {
            transform.localPosition = new Vector3(-0.2f, 1.25f, 0.25f);
        }
    }
}
