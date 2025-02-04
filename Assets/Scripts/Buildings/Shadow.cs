using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UI;

public class Shadow : MonoBehaviour
{
    public bool isCutted;
    public SpriteRenderer spriteRenderer;
    public Sprite sprite;

    [SerializeField] Transform buildingTransform;
    [SerializeField] Sprite curSprite;
    [SerializeField] Sprite[] cuttedSprites;
 
    private void Update()
    {
        if (buildingTransform.position.y > BuildingManager.instance.standardHeight)
        {
            spriteRenderer.sprite = null;
        }
        else
        {
            if (!isCutted) 
            {
                spriteRenderer.sprite = sprite;
            }
            else
            {
                spriteRenderer.sprite = curSprite;
            }
        }
    }

    public void CutSprite(bool isXType, bool isInput, bool isReversed)
    {
        if (!isCutted)
        {
            if (isXType && isInput == true)
            {
                curSprite = cuttedSprites[3];
            }

            if (!isXType && isInput == true)
            {
                curSprite = cuttedSprites[0];
            }

            if (isXType && isInput == false)
            {
                curSprite = cuttedSprites[2];
            }

            if (!isXType && isInput == false)
            {
                curSprite = cuttedSprites[1];
            }

            spriteRenderer.sprite = curSprite;

            isCutted = true;

            if (isReversed)
            {
                return;
            }

            if (isXType && isInput == true)
            {
                curSprite = cuttedSprites[2];
            }

            if (!isXType && isInput == true)
            {
                curSprite = cuttedSprites[1];
            }

            if (isXType && isInput == false)
            {
                curSprite = cuttedSprites[3];
            }

            if (!isXType && isInput == false)
            {
                curSprite = cuttedSprites[0];
            }

            spriteRenderer.sprite = curSprite;

            isCutted = true;
        }
    }
}
