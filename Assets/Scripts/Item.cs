using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    public bool isMoving;
    public Transform spriteTransform;
    public SpriteRenderer spriteRenderer;
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
        .Append(spriteTransform.DOScale(new Vector3(1, 1, 1), endScaleTime).SetEase(endScaleEase));
    }
}
