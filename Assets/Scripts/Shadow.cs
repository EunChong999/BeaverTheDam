using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UI;

public class Shadow : MonoBehaviour
{
    public bool isCutted;

    [SerializeField] Transform buildingTransform;
    [SerializeField] SpriteRenderer spriteRenderer;
    [SerializeField] Sprite sprite;
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

    public void CutSprite(bool isXType, cutterType cutterType)
    {
        if (!isCutted)
        {
            if (isXType && cutterType == cutterType.inputType)
            {
                curSprite = cuttedSprites[3];
            }

            if (!isXType && cutterType == cutterType.inputType)
            {
                curSprite = cuttedSprites[0];
            }

            if (isXType && cutterType == cutterType.outputType)
            {
                curSprite = cuttedSprites[2];
            }

            if (!isXType && cutterType == cutterType.outputType)
            {
                curSprite = cuttedSprites[1];
            }

            spriteRenderer.sprite = curSprite;

            isCutted = true;
        }
    }
}
