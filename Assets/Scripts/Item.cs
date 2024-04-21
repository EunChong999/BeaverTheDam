using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    public bool isCutted;
    public bool isPainted;
    public bool isMoving;
    public Transform spriteTransform;
    public Point curPoint;
    public SpriteRenderer spriteRenderer;
    public Sprite replaceSprite;
    public Sprite[] cuttedSprites;
    public Sprite[] cuttedReplaceSprite;

    Sequence itemScaleSequence;
    Vector3 originScale;

    [SerializeField] float startScaleTime;
    [SerializeField] float endScaleTime;
    [SerializeField] Ease rotationEase;
    [SerializeField] Ease startScaleEase;
    [SerializeField] Ease endScaleEase;

    private void OnEnable()
    {
        originScale = spriteTransform.localScale;
    }

    #region Functions
    public void EnMove()
    {
        isMoving = true;
    }

    public void UnMove()
    {
        isMoving = false;
    }

    /// <summary>
    /// 회전시 트위닝 효과를 주는 함수
    /// </summary>
    public void ShowEffect(bool isSending)
    {
        if (isSending)
        {
            itemScaleSequence = DOTween.Sequence().SetAutoKill(true)
            .Append(spriteTransform.DOScale(new Vector3(spriteTransform.localScale.x / 1.5f, spriteTransform.localScale.y / 1.25f, spriteTransform.localScale.z / 1.5f), startScaleTime).SetEase(startScaleEase))
            .Append(spriteTransform.DOScale(originScale, endScaleTime).SetEase(endScaleEase));
        }
        else
        {
            itemScaleSequence = DOTween.Sequence().SetAutoKill(true)
            .Append(spriteTransform.DOScale(new Vector3(spriteTransform.localScale.x / 1.5f, spriteTransform.localScale.y / 1.25f, spriteTransform.localScale.z / 1.5f), startScaleTime).SetEase(startScaleEase))
            .Append(spriteTransform.DOScale(originScale, endScaleTime * 0.75f).SetEase(endScaleEase));
        }
    }

    public void CutSprite(bool isXType, cutterType cutterType)
    {
        if (!isCutted)
        {
            if (isXType && cutterType == cutterType.inputType)
            {
                spriteRenderer.sprite = cuttedSprites[3];
            }

            if (!isXType && cutterType == cutterType.inputType)
            {
                spriteRenderer.sprite = cuttedSprites[0];
            }

            if (isXType && cutterType == cutterType.outputType)
            {
                spriteRenderer.sprite = cuttedSprites[2];
            }

            if (!isXType && cutterType == cutterType.outputType)
            {
                spriteRenderer.sprite = cuttedSprites[1];
            }

            if (isPainted)
            {
                if (spriteRenderer.sprite == cuttedSprites[0])
                {
                    spriteRenderer.sprite = cuttedReplaceSprite[0];
                }

                if (spriteRenderer.sprite == cuttedSprites[1])
                {
                    spriteRenderer.sprite = cuttedReplaceSprite[1];
                }

                if (spriteRenderer.sprite == cuttedSprites[2])
                {
                    spriteRenderer.sprite = cuttedReplaceSprite[2];
                }

                if (spriteRenderer.sprite == cuttedSprites[3])
                {
                    spriteRenderer.sprite = cuttedReplaceSprite[3];
                }
            }

            isCutted = true;
        }
    }

    public void PaintSprite(Color color)
    {
        spriteRenderer.sprite = replaceSprite;

        if (isCutted)
        {
            if (spriteRenderer.sprite == cuttedSprites[0])
            {
                spriteRenderer.sprite = cuttedReplaceSprite[0];
            }

            if (spriteRenderer.sprite == cuttedSprites[1])
            {
                spriteRenderer.sprite = cuttedReplaceSprite[1];
            }

            if (spriteRenderer.sprite == cuttedSprites[2])
            {
                spriteRenderer.sprite = cuttedReplaceSprite[2];
            }

            if (spriteRenderer.sprite == cuttedSprites[3])
            {
                spriteRenderer.sprite = cuttedReplaceSprite[3];
            }
        }

        isPainted = true;
    }

    public Sprite ApplyCutSprites(bool isXType, cutterType cutterType)
    {
        if (isXType && cutterType == cutterType.inputType)
        {
            spriteRenderer.sprite = cuttedSprites[3];
        }

        if (!isXType && cutterType == cutterType.inputType)
        {
            spriteRenderer.sprite = cuttedSprites[0];
        }

        if (isXType && cutterType == cutterType.outputType)
        {
            spriteRenderer.sprite = cuttedSprites[2];
        }

        if (!isXType && cutterType == cutterType.outputType)
        {
            spriteRenderer.sprite = cuttedSprites[1];
        }

        if (isPainted)
        {
            if (spriteRenderer.sprite == cuttedSprites[0])
            {
                spriteRenderer.sprite = cuttedReplaceSprite[0];
            }

            if (spriteRenderer.sprite == cuttedSprites[1])
            {
                spriteRenderer.sprite = cuttedReplaceSprite[1];
            }

            if (spriteRenderer.sprite == cuttedSprites[2])
            {
                spriteRenderer.sprite = cuttedReplaceSprite[2];
            }

            if (spriteRenderer.sprite == cuttedSprites[3])
            {
                spriteRenderer.sprite = cuttedReplaceSprite[3];
            }
        }

        return spriteRenderer.sprite;
    }
    #endregion
}
