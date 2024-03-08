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

    int rotationToInt;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        moveType = basicBuilding.moveType;
    }

    void LateUpdate()
    {
        transform.localRotation = spriteTransform.localRotation;
        rotationToInt = Mathf.RoundToInt(transform.parent.rotation.eulerAngles.y);

        switch (moveType)
        {
            case moveType.straightType:
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
                break;
            case moveType.curveType:
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
                break;
        }

        if (rotationToInt >= 0 && rotationToInt < 90)
        {
            transform.localPosition = new Vector3(-0.1f, 1.25f, -0.125f);
        }
        else if (rotationToInt >= 90 && rotationToInt < 180)
        {
            transform.localPosition = new Vector3(0.1f, 1.25f, -0.125f);
        }
        else if (rotationToInt >= 180 && rotationToInt < 270)
        {
            transform.localPosition = new Vector3(0.1f, 1.25f, 0.125f);
        }
        else if (rotationToInt >= 270 && rotationToInt < 360)
        {
            transform.localPosition = new Vector3(-0.1f, 1.25f, 0.125f);
        }
    }
}
