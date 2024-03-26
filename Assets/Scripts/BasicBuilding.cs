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

    #endregion
    #region Functions
    /// <summary>
    /// 기본 설정들을 초기화하는 함수
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
    /// 회전에 대한 전체적인 동작을 지시하는 함수
    /// </summary>
    public void DirectRotation(bool isRight, float targetAngle)
    {
        if (!isRotating && canRotate)
        {
            RotateTransform(isRight, targetAngle, transform);
            ShowEffect();
            StartCoroutine(InitToOriginValue());
            StartCoroutine(SetArrowDirection());
            directionObj.SetActive(false);
            isRotating = true;
        }
    }

    /// <summary>
    /// 카메라를 바라보는 함수
    /// </summary>
    protected void LookCameraRotation()
    {
        spriteTransform.LookAt(spriteTransform.position + Camera.main.transform.rotation * Vector3.forward, Camera.main.transform.rotation * Vector3.up);
    }

    /// <summary>
    /// 회전시 트위닝 효과를 주는 함수
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
    /// 건물을 회전시키는 함수
    /// </summary>
    protected void RotateTransform(bool isRight, float targetAngle, Transform transform)
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
    /// 회전을 통해 변한 값을 초기화하는 함수
    /// </summary>
    IEnumerator InitToOriginValue()
    {
        yield return waitForRotationSeconds;
        isRotating = false;
        transform.eulerAngles = targetRotation;
    }

    /// <summary>
    /// 화살표의 방향을 설정하는 함수
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

        if (buildingType == buildingType.movableType) 
        {
            if (movementType == movementType.curveType)
            {
                if (directionType == directionType.rightType)
                {
                    directionType = directionType.leftType;
                    RotateTransform(true, 90, pointTransform);
                }
                else
                {
                    directionType = directionType.rightType;
                    RotateTransform(true, -90, pointTransform);
                }
            }
            else
            {
                DirectRotation(true, 180);
            }
        }
        else
        {
            if (movementType == movementType.curveType)
            {
                if (directionType == directionType.rightType)
                {
                    directionType = directionType.leftType;
                    RotateTransform(true, -90, transform);
                    RotateTransform(true, 0, pointTransform);
                }
                else
                {
                    directionType = directionType.rightType;
                    RotateTransform(true, 90, transform);
                    RotateTransform(true, 0, pointTransform);
                }
            }
            else
            {
                DirectRotation(true, 180);
            }
        }
    }

    #endregion
}
