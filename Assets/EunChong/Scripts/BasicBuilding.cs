using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    Sequence scaleSequence;
    #endregion

    #region Functions
    /// <summary>
    /// ȸ���� ���� ��ü���� ������ �����ϴ� �Լ�
    /// </summary>
    public void DirectRotation()
    {
        if (!isRotating)
        {
            RotateTransform();
            ShowEffect();

            Invoke(nameof(ApplyChangedValue), rotationTime);

            isRotating = true;
        }
    }

    /// <summary>
    /// ȸ���� ���� �������� �ʱ�ȭ�ϴ� �Լ�
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
    /// ī�޶� �ٶ󺸴� �Լ�
    /// </summary>
    public void LookCameraRotation()
    {
        spriteTransform.LookAt(spriteTransform.position + Camera.main.transform.rotation * Vector3.forward, Camera.main.transform.rotation * Vector3.up);
    }

    /// <summary>
    /// ȸ���� Ʈ���� ȿ���� �ִ� �Լ�
    /// </summary>
    public void ShowEffect()
    {
        scaleSequence = DOTween.Sequence().SetAutoKill(false)
        .Append(transform.DOScale(new Vector3(transform.localScale.x / 1.5f, transform.localScale.y / 1.25f, transform.localScale.z / 1.5f), startScaleTime).SetEase(startScaleEase))
        .Append(transform.DOScale(originScale, endScaleTime).SetEase(endScaleEase));
    }

    /// <summary>
    /// ȸ������ ���� ��������Ʈ�� �����ϴ� �Լ�
    /// </summary>
    public void ChangeSprite()
    {
        spriteAnimator.SetFloat("Angle", transform.eulerAngles.y);
    }

    /// <summary>
    /// Transform�� ȸ����Ű�� �Լ�
    /// </summary>
    public void RotateTransform()
    {
        Vector3 targetRotation = transform.eulerAngles + new Vector3(0, 90, 0);
        transform.DORotate(targetRotation, rotationTime).SetEase(rotationEase);
    }

    /// <summary>
    /// ȸ���� ���� ���� ���� �����ϴ� �Լ�
    /// </summary>
    public void ApplyChangedValue()
    {
        isRotating = false;
    }

    public void CheckCanMove()
    {
        canMove = pointClass.canMove;
    }
    #endregion
}
