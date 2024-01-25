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
    public Transform spriteTransform;
    public Transform pointTransform;
    public GameObject directionObj;
    public bool isRotating;
    public bool canMove;
    public bool isItemExist;
    public moveType moveType;

    [SerializeField] Ease rotationEase;
    [SerializeField] float startScaleTime;
    [SerializeField] float endScaleTime;
    [SerializeField] Ease startScaleEase;
    [SerializeField] Ease endScaleEase;

    [HideInInspector] public Vector3 originScale;
    [HideInInspector] public Animator spriteAnimator;
    [HideInInspector] public Point point;

    WaitForSeconds waitForSeconds;
    Sequence scaleSequence;
    Vector3 targetRotation;

    #endregion
    #region Functions
    /// <summary>
    /// ȸ���� ���� �������� �ʱ�ȭ�ϴ� �Լ�
    /// </summary>
    protected void InitSettings()
    {
        waitForSeconds = new WaitForSeconds(rotationTime);
        originScale = transform.localScale;
        spriteTransform = transform.Find("Sprite");
        spriteAnimator = spriteTransform.GetComponent<Animator>();
        pointTransform = transform.Find("Point");
        point = pointTransform.GetComponent<Point>();
    }

    /// <summary>
    /// ȸ���� ���� ��ü���� ������ �����ϴ� �Լ�
    /// </summary>
    protected void DirectRotation()
    {
        if (!isRotating)
        {
            RotateTransform();
            ShowEffect();
            StartCoroutine(InitToOriginValue());

            isRotating = true;
        }
    }

    /// <summary>
    /// ī�޶� �ٶ󺸴� �Լ�
    /// </summary>
    protected void LookCameraRotation()
    {
        spriteTransform.LookAt(spriteTransform.position + Camera.main.transform.rotation * Vector3.forward, Camera.main.transform.rotation * Vector3.up);
    }

    /// <summary>
    /// ȸ���� Ʈ���� ȿ���� �ִ� �Լ�
    /// </summary>
    protected void ShowEffect()
    {
        scaleSequence = DOTween.Sequence().SetAutoKill(true)
        .Append(transform.DOScale(new Vector3(transform.localScale.x / 1.5f, transform.localScale.y / 1.25f, transform.localScale.z / 1.5f), startScaleTime).SetEase(startScaleEase))
        .Append(transform.DOScale(originScale, endScaleTime).SetEase(endScaleEase));
    }

    /// <summary>
    /// ȸ������ ���� ��������Ʈ�� �����ϴ� �Լ�
    /// </summary>
    protected void ChangeSprite()
    {
        spriteAnimator.SetFloat("Angle", transform.eulerAngles.y);
    }

    /// <summary>
    /// Transform�� ȸ����Ű�� �Լ�
    /// </summary>
    protected void RotateTransform()
    {
        targetRotation = transform.eulerAngles + new Vector3(0, 90, 0);
        transform.DORotate(targetRotation, rotationTime).SetEase(rotationEase);
    }

    /// <summary>
    /// ȸ���� ���� ���� ���� �ʱ�ȭ�ϴ� �Լ�
    /// </summary>
    IEnumerator InitToOriginValue()
    {
        yield return waitForSeconds;
        isRotating = false;
        transform.eulerAngles = targetRotation;
    }

    /// <summary>
    /// ���� ����Ʈ�� �̵��� �� �ִ��� Ȯ���ϴ� �Լ�
    /// </summary>
    protected void CheckCanMove()
    {
        canMove = point.canMove;
    }

    /// <summary>
    /// ����Ʈ ���� �������� �����ϴ��� Ȯ���ϴ� �Լ�
    /// </summary>
    protected void CheckIsItemExist()
    {
        isItemExist = point.isItemExist;
    }

    /// <summary>
    /// Ʈ���Ϸ��� Ÿ���� �����ϴ� �Լ�
    /// </summary>
    protected void SetTrailerType()
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

    protected void SetArrowDirection()
    {
        if (isRotating)
        {
            directionObj.SetActive(false);
        }
        else
        {
            directionObj.SetActive(true);
        }
    }

    #endregion
}
