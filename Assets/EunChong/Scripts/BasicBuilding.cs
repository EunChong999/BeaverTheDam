using DG.Tweening;
using System.Collections;
using UnityEngine;

public enum buildingType
{
    movableType,
    fixedType
}

public class BasicBuilding : MonoBehaviour
{
    #region Variables
    [Header("BasicBuilding")]

    [Space(10)]

    public buildingType buildingType;   
    public float rotationTime;
    public Transform spriteTransform;
    public Transform pointTransform;
    public GameObject directionObj;
    public bool isRotating;

    [SerializeField] Ease rotationEase;
    [SerializeField] float startScaleTime;
    [SerializeField] float endScaleTime;
    [SerializeField] Ease startScaleEase;
    [SerializeField] Ease endScaleEase;

    [HideInInspector] public Vector3 originScale;
    [HideInInspector] public Animator spriteAnimator;
    [HideInInspector] public Point point;

    WaitForSeconds waitForSeconds;
    Sequence buildingScaleSequence;
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
        spriteAnimator = spriteTransform.GetComponent<Animator>();
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
    /// ȭ��ǥ�� ������ �����ϴ� �Լ�
    /// </summary>
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
