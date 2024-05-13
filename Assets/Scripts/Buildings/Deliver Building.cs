using System.Collections;
using UnityEngine;

public class DeliverBuilding : BasicBuilding, ISendableBuilding, IInputableBuilding
{
    #region Variables

    [Header("DeliverBuilding")]

    [Space(10)]

    [SerializeField] Delivers delivers;
    [SerializeField] float floatTime;
    [SerializeField] float arriveTime;
    [SerializeField] float throwTime;
    [SerializeField] float height;
    [SerializeField] float speed;

    float startTime;
    Vector3 centerPoint;
    Vector3 startRelCenter;
    Vector3 endRelCenter;
    Transform itemTransform;
    Transform startPos;
    Transform endPos;
    WaitForSeconds waitForArriveSeconds;
    WaitForSeconds waitForThrowSeconds;
    bool isArrived;
    bool isDelivered;

    #endregion
    #region Functions
    /// <summary>
    /// 기본 설정들을 초기화하는 함수
    /// </summary>
    public override void InitSettings()
    {
        base.InitSettings();
        waitForArriveSeconds = new WaitForSeconds(arriveTime);
        waitForThrowSeconds = new WaitForSeconds(throwTime);
    }

    /// <summary>
    /// 아이템을 발사하는 함수
    /// </summary>
    public IEnumerator Input()
    {
        if (pointingPoint != null && pointingPoint.hitTransform != null && pointingPoint.itemTransform != null)
        {
            if (pointingPoint.hitTransform.Equals(pointTransform) &&
                pointingPoint.canMove &&
                !isDelivered &&
                pointingPoint.isItemExist &&
                !pointingPoint.itemTransform.GetComponent<Item>().isMoving &&
                pointingPoint.transform.parent.GetComponent<BasicBuilding>().buildingType == buildingType.movableType &&
                itemTransform == null)
            {
                isArrived = false;
                itemTransform = pointingPoint.itemTransform;
                startPos = pointingPoint.transform.parent.GetComponent<BasicBuilding>().pointTransform;
                endPos = pointTransform;
                yield return waitForThrowSeconds;
                StartCoroutine(GetCenter(Vector3.up / (height * Vector3.Distance(startPos.position, endPos.position))));
                StartCoroutine(ThrowItem(itemTransform));
                StartCoroutine(WaitForInput());
                isDelivered = true;
            }
        }
    }

    /// <summary>
    /// 포물선의 중앙을 결정하는 함수
    /// </summary>
    public IEnumerator GetCenter(Vector3 direction)
    {
        while (!isArrived)
        {
            centerPoint = (startPos.position + endPos.position) * .5f;
            centerPoint -= direction;
            startRelCenter = startPos.position - centerPoint;
            endRelCenter = endPos.position - centerPoint;
            yield return null;
        }
    }

    /// <summary>
    /// 아이템을 발사하는 함수
    /// </summary>
    public IEnumerator ThrowItem(Transform item)
    {
        float time = 0;

        while (!isArrived && item != null)
        {
            time += Time.deltaTime;
            float fracComplete = (time - startTime) / floatTime * speed;
            item.position = Vector3.Slerp(startRelCenter, endRelCenter, fracComplete * speed);
            item.position += centerPoint;
            yield return null;
        }
    }

    /// <summary>
    /// 이동을 대기시키는 함수
    /// </summary>
    public IEnumerator WaitForInput()
    {
        yield return waitForArriveSeconds;
        isArrived = true;
        point.isItemExist = false;

        if (itemTransform != null)
        {
            itemTransform.gameObject.SetActive(false);
            delivers.AcceptItem(itemTransform.gameObject.name);
            itemTransform = null;
        }

        isDelivered = false;
    }

    /// <summary>
    /// 애니메이션을 재생하는 함수
    /// </summary>
    private void PlayAnimation()
    {
        animator.SetFloat("Angle", Mathf.RoundToInt(transform.eulerAngles.y));
    }
    #endregion
    #region Events
    protected override void Start()
    {
        base.Start();
        InitSettings();
    }

    private void Update()
    {
        DirectRotation(false, targetAngle, transform, false);
        StartCoroutine(Input());
        PlayAnimation();
    }

    private void LateUpdate()
    {
        LookCameraRotation();
    }
    #endregion
}