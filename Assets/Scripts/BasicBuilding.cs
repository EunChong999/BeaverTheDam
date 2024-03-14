using DG.Tweening;
using System.Collections;
using UnityEngine;

public enum buildingType
{
    movableType,
    fixedType
}

public enum movementType
{
    straightType,
    curveType
}

public enum directionType
{
    none,
    leftType,
    rightType,
}

public class BasicBuilding : MonoBehaviour
{
    #region Variables
    [Header("BasicBuilding")]

    [Space(10)]

    public buildingType buildingType;
    public movementType movementType;
    public directionType directionType;
    public float rotationTime;
    public float directionTime;
    public Transform spriteTransform;
    public Transform pointTransform;
    public GameObject directionObj;
    public Point pointingPoint;
    public bool isRotating;
    public bool canRotate;

    [SerializeField] protected float targetAngle;

    [SerializeField] Ease rotationEase;
    [SerializeField] float startScaleTime;
    [SerializeField] float endScaleTime;
    [SerializeField] Ease startScaleEase;
    [SerializeField] Ease endScaleEase;

    [HideInInspector] public Vector3 originScale;
    [HideInInspector] public Animator spriteAnimator;
    [HideInInspector] public Point point;

    WaitForSeconds waitForRotationSeconds;
    WaitForSeconds waitForDirectionSeconds;
    Sequence buildingScaleSequence;
    Vector3 targetRotation;

    bool isRightMoving;

    #endregion
    #region Functions
    /// <summary>
    /// �⺻ �������� �ʱ�ȭ�ϴ� �Լ�
    /// </summary>
    public virtual void InitSettings()
    {
        waitForRotationSeconds = new WaitForSeconds(rotationTime);
        waitForDirectionSeconds = new WaitForSeconds(directionTime);
        originScale = transform.localScale;
        spriteAnimator = spriteTransform.GetComponent<Animator>();
        point = pointTransform.GetComponent<Point>();
    }

    /// <summary>
    /// ȸ���� ���� ��ü���� ������ �����ϴ� �Լ�
    /// </summary>
    public void DirectRotation(bool isRight, float targetAngle)
    {
        if (!isRotating && canRotate)
        {
            isRightMoving = isRight; 
            RotateTransform(isRight, targetAngle);
            ShowEffect();
            StartCoroutine(InitToOriginValue());
            StartCoroutine(SetArrowDirection());
            directionObj.SetActive(false);
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
        buildingScaleSequence = DOTween.Sequence().SetAutoKill(true)
        .Append(transform.DOScale(new Vector3(transform.localScale.x / 1.5f, transform.localScale.y / 1.25f, transform.localScale.z / 1.5f), startScaleTime).SetEase(startScaleEase))
        .Append(transform.DOScale(originScale, endScaleTime).SetEase(endScaleEase));

        if (pointTransform.GetComponent<Point>().itemTransform != null)
        {
            pointTransform.GetComponent<Point>().ShowEffect();
        }
    }

    /// <summary>
    /// �ǹ��� ȸ����Ű�� �Լ�
    /// </summary>
    protected void RotateTransform(bool isRight, float targetAngle)
    {
        if (isRight)
        {
            targetRotation = transform.eulerAngles + new Vector3(0, targetAngle, 0);
        }
        else
        {
            targetRotation = transform.eulerAngles + new Vector3(0, -targetAngle, 0);
        }

        transform.DORotate(targetRotation, rotationTime).SetEase(rotationEase);
    }

    /// <summary>
    /// ȸ���� ���� ���� ���� �ʱ�ȭ�ϴ� �Լ�
    /// </summary>
    IEnumerator InitToOriginValue()
    {
        yield return waitForRotationSeconds;
        isRotating = false;
        transform.eulerAngles = targetRotation;
    }

    /// <summary>
    /// ȭ��ǥ�� ������ �����ϴ� �Լ�
    /// </summary>
    IEnumerator SetArrowDirection()
    {
        yield return waitForDirectionSeconds;

        if (!isRotating) 
        {
            directionObj.SetActive(true);
        }
    }

    public void ChangeDirectionType()
    {
        ShowEffect();

        if (movementType == movementType.curveType)
        {
            if (directionType == directionType.rightType)
            {
                directionType = directionType.leftType;
            }
            else
            {
                directionType = directionType.rightType;
            }
        }
        else
        {
            DirectRotation(isRightMoving, 180);
        }
    }

    #endregion
}
