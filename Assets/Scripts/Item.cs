using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    public bool isCutted;
    public bool isColored;
    public bool isMoving;
    public Transform spriteTransform;
    public SpriteRenderer spriteRenderer;
    public Sprite replaceSprite;
    public Sprite[] cuttedSprites;
    public Sprite[] cuttedReplaceSprite;
    Sequence itemScaleSequence;

    [SerializeField] float startScaleTime;
    [SerializeField] float endScaleTime;
    [SerializeField] Ease rotationEase;
    [SerializeField] Ease startScaleEase;
    [SerializeField] Ease endScaleEase;

    private void Start()
    {
        spriteTransform.localScale = Vector3.zero;
    }

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
    public void ShowEffect()
    {
        itemScaleSequence = DOTween.Sequence().SetAutoKill(true)
        .Append(spriteTransform.DOScale(new Vector3(spriteTransform.localScale.x / 1.5f, spriteTransform.localScale.y / 1.25f, transform.localScale.z / 1.5f), startScaleTime).SetEase(startScaleEase))
        .Append(spriteTransform.DOScale(new Vector3(2, 2, 2), endScaleTime).SetEase(endScaleEase));
    }

    public void CutSprite(bool isXType, bool canStore)
    {
        if (isCutted)
        {
            return;
        }

        if (isXType && canStore)
        {
            spriteRenderer.sprite = cuttedSprites[3];
        }

        if (!isXType && canStore)
        {
            spriteRenderer.sprite = cuttedSprites[0];
        }

        if (isXType && !canStore)
        {
            spriteRenderer.sprite = cuttedSprites[2];
        }

        if (!isXType && !canStore)
        {
            spriteRenderer.sprite = cuttedSprites[1];
        }

        isCutted = true;
    }

    public void ReplaceSprite()
    {
        spriteRenderer.sprite = replaceSprite;

        isColored = true;
    }

    public void ReplaceSprites()
    {
        if (!isColored)
        {
            return;
        }

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
}
