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
    public float rotationTime = 0.15f;
    public float directionTime = 0.25f;
    public Transform spriteTransform;
    public Transform pointTransform;
    public Point pointingPoint;
    public Detector detector;
    public bool isRotating = false;
    public bool canRotate = true;

    [SerializeField] protected float targetAngle = 90;

    [SerializeField] Ease rotationEase = Ease.Linear;
    [SerializeField] float startScaleTime = 0.1f;
    [SerializeField] float endScaleTime = 0.5f;
    [SerializeField] Ease startScaleEase = Ease.OutSine;
    [SerializeField] Ease endScaleEase = Ease.OutElastic;

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
    public void DirectRotation(bool isRight, float targetAngle, Transform transform)
    {
        if (canRotate)
        {
            ShowEffect();
            StartCoroutine(RotateTransform(isRight, targetAngle, transform));
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
    private IEnumerator RotateTransform(bool isRight, float targetAngle, Transform transform)
    {
        isRotating = true;

        if (isRight)
        {
            targetRotation = transform.eulerAngles + new Vector3(0, targetAngle, 0);
        }
        else
        {
            targetRotation = transform.eulerAngles + new Vector3(0, -targetAngle, 0);
        }

        transform.DORotate(targetRotation, rotationTime - 0.25f).SetEase(rotationEase);

        yield return waitForRotationSeconds;

        transform.eulerAngles = targetRotation;

        isRotating = false;
    }

    /// <summary>
    /// 회전 타입을 변경하는 함수
    /// </summary>
    public void ChangeDirectionType()
    {
        ShowEffect();

        if (movementType == movementType.curveType)
        {
            if (directionType == directionType.rightType)
            {
                directionType = directionType.leftType;
                DirectRotation(true, 90, pointTransform);
            }
            else
            {
                directionType = directionType.rightType;
                DirectRotation(true, -90, pointTransform);
            }
        }
        else
        {
            DirectRotation(true, 180, transform);
        }
    }
    #endregion
}
