using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum moveType
{
    straight,
    curve
}

public class BasicBuilding : MonoBehaviour
{
    #region Variables
    [Header("BasicBuilding")]

    [Space(10)]

    public float rotationTime;

    [SerializeField] Ease rotationEase;
    [SerializeField] float startScaleTime;
    [SerializeField] float endScaleTime;
    [SerializeField] Ease startScaleEase;
    [SerializeField] Ease endScaleEase;

    [HideInInspector] public bool isRotating;
    [HideInInspector] public Vector3 originScale;
    [HideInInspector] public Animator spriteAnimator;
    [HideInInspector] public Transform spriteTransform;
    [HideInInspector] public Transform pointTransform;
    [HideInInspector] public Point pointClass;

    public bool canMove;
    public moveType moveType;

    Sequence scaleSequence;
    Vector3 targetRotation;
    #endregion

    #region Functions
    /// <summary>
    /// 회전에 대한 전체적인 동작을 지시하는 함수
    /// </summary>
    public void DirectRotation()
    {
        if (!isRotating)
        {
            RotateTransform();
            ShowEffect();

            Invoke(nameof(InitToOriginValue), rotationTime);

            isRotating = true;
        }
    }

    /// <summary>
    /// 회전을 위한 설정들을 초기화하는 함수
    /// </summary>
    public void InitSettings()
    {
        originScale = transform.localScale;
        spriteTransform = transform.Find("Sprite");
        spriteAnimator = spriteTransform.GetComponent<Animator>();
        pointTransform = transform.Find("Point");
        pointClass = pointTransform.GetComponent<Point>();  
    }

    /// <summary>
    /// 카메라를 바라보는 함수
    /// </summary>
    public void LookCameraRotation()
    {
        spriteTransform.LookAt(spriteTransform.position + Camera.main.transform.rotation * Vector3.forward, Camera.main.transform.rotation * Vector3.up);
    }

    /// <summary>
    /// 회전시 트위닝 효과를 주는 함수
    /// </summary>
    public void ShowEffect()
    {
        scaleSequence = DOTween.Sequence().SetAutoKill(true)
        .Append(transform.DOScale(new Vector3(transform.localScale.x / 1.5f, transform.localScale.y / 1.25f, transform.localScale.z / 1.5f), startScaleTime).SetEase(startScaleEase))
        .Append(transform.DOScale(originScale, endScaleTime).SetEase(endScaleEase));
    }

    /// <summary>
    /// 회전각에 따라 스프라이트를 변경하는 함수
    /// </summary>
    public void ChangeSprite()
    {
        spriteAnimator.SetFloat("Angle", transform.eulerAngles.y);
    }

    /// <summary>
    /// Transform을 회전시키는 함수
    /// </summary>
    public void RotateTransform()
    {
        targetRotation = transform.eulerAngles + new Vector3(0, 90, 0);
        transform.DORotate(targetRotation, rotationTime).SetEase(rotationEase);
    }

    /// <summary>
    /// 회전을 통해 변한 값을 초기화하는 함수
    /// </summary>
    public void InitToOriginValue()
    {
        isRotating = false;
        transform.eulerAngles = targetRotation;
    }

    /// <summary>
    /// 다음으로 이동할 수 있는지 확인하는 함수
    /// </summary>
    public void CheckCanMove()
    {
        canMove = pointClass.canMove;
    }

    /// <summary>
    /// 트레일러의 타입을 선택하는 함수
    /// </summary>
    public void SelectTrailerType()
    {
        switch (moveType)
        {
            case moveType.straight:
                spriteAnimator.SetBool("IsStraight", true);
                break;
            case moveType.curve:
                spriteAnimator.SetBool("IsStraight", false);
                break;
        }
    }
    #endregion
}
