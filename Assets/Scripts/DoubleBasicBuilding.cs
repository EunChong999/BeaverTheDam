using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoubleBasicBuilding : MonoBehaviour
{
    [SerializeField] GameObject[] buildings;
    [SerializeField] Transform spriteTransform;
    [HideInInspector] public Vector3 originScale;
    Sequence buildingScaleSequence;

    [SerializeField] float targetAngle;
    [SerializeField] Ease rotationEase;
    [SerializeField] float startScaleTime;
    [SerializeField] float endScaleTime;
    [SerializeField] Ease startScaleEase;
    [SerializeField] Ease endScaleEase;

    private void Start()
    {
        originScale = spriteTransform.localScale;
    }

    private void OnMouseOver()
    {
        // 마우스 좌클릭
        if (Input.GetMouseButtonDown(0))
        {
            ShowEffect();
            buildings[0].GetComponent<BasicBuilding>().DirectRotation(directionType.leftType);
            buildings[1].GetComponent<BasicBuilding>().DirectRotation(directionType.leftType);
        }

        // 마우스 우클릭
        if (Input.GetMouseButtonDown(1))
        {
            ShowEffect();
            buildings[0].GetComponent<BasicBuilding>().DirectRotation(directionType.rightType);
            buildings[1].GetComponent<BasicBuilding>().DirectRotation(directionType.rightType);
        }
    }

    /// <summary>
    /// 회전시 트위닝 효과를 주는 함수
    /// </summary>
    protected void ShowEffect()
    {
        buildingScaleSequence = DOTween.Sequence().SetAutoKill(true)
        .Append(spriteTransform.DOScale(new Vector3(spriteTransform.localScale.x / 1.5f, spriteTransform.localScale.y / 1.25f, spriteTransform.localScale.z / 1.5f), startScaleTime).SetEase(startScaleEase))
        .Append(spriteTransform.DOScale(originScale, endScaleTime).SetEase(endScaleEase));
    }
}
