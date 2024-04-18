using System.Collections;
using UnityEngine;

public class CutterBuilding : BasicBuilding
{
    #region Variables
    [Header("CutterBuilding")]

    [Space(10)]

    [SerializeField] CutterBuilding partnerBuilding;
    [SerializeField] bool canStore;
    [SerializeField] float floatTime;
    [SerializeField] float arriveTime;
    [SerializeField] float spawnTime;
    [SerializeField] float returnTime;
    [SerializeField] float storeTime;
    [SerializeField] float height;
    [SerializeField] float speed;
    [SerializeField] bool isXType;

    float startTime;
    Vector3 centerPoint;
    Vector3 startRelCenter;
    Vector3 endRelCenter;
    Transform itemTransform;
    Transform startPos;
    Transform endPos;
    GameObject itemTemp;
    WaitForSeconds waitForArriveSeconds;
    WaitForSeconds waitForSpawnSeconds;
    WaitForSeconds waitForReturnSeconds;
    WaitForSeconds waitForStoreSeconds;
    bool isArrived;
    bool isReturned;
    bool isRemoved;
    bool isStoring;

    #endregion
    #region Functions
    public override void InitSettings()
    {
        base.InitSettings();
        waitForArriveSeconds = new WaitForSeconds(arriveTime);
        waitForSpawnSeconds = new WaitForSeconds(spawnTime);
        waitForReturnSeconds = new WaitForSeconds(returnTime);
        waitForStoreSeconds = new WaitForSeconds(storeTime);
    }

    private void DirectStoreItem()
    {
        if (pointingPoint != null && pointingPoint.hitTransform != null && pointingPoint.itemTransform != null)
        {
            if (pointingPoint.hitTransform.Equals(pointTransform) &&
                pointingPoint.canMove &&
                !isRemoved &&
                pointingPoint.isItemExist &&
                !pointingPoint.itemTransform.GetComponent<Item>().isMoving &&
                itemTemp == null)
            {
                startPos = pointingPoint.transform.parent.GetComponent<BasicBuilding>().pointTransform;
                endPos = pointTransform;
                isStoring = true;
                isArrived = false;
                canRotate = false;
                itemTransform = pointingPoint.itemTransform;
                itemTemp = itemTransform.gameObject;
                StartCoroutine(GetCenter(Vector3.up / (height * Vector3.Distance(startPos.position, endPos.position))));
                StartCoroutine(ThrowItem(itemTransform));
                StartCoroutine(WaitMoveForStore());
                isRemoved = true;
            }
        }
    }

    /// <summary>
    /// 포물선의 중앙을 결정하는 함수
    /// </summary>
    private IEnumerator GetCenter(Vector3 direction)
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
    private IEnumerator ThrowItem(Transform item)
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
    private IEnumerator WaitMoveForStore()
    {
        yield return waitForArriveSeconds;
        isArrived = true;
        canRotate = true;
        itemTemp.SetActive(false);
        point.isItemExist = false;
        isRemoved = false;
        yield return waitForStoreSeconds;
        isStoring = false;
    }

    /// <summary>
    /// 이동을 대기시키는 함수
    /// </summary>
    private IEnumerator WaitMoveForReturn()
    {
        yield return waitForArriveSeconds;
        isArrived = true;
        canRotate = true;
        yield return waitForReturnSeconds;
        isReturned = false;
        itemTemp = null;
    }

    private void DirectReturnItem()
    {
        if (itemTemp != null &&
            !isRotating &&
            point.canMove &&
            !isReturned &&
            !point.hitTransform.GetComponent<Point>().isItemExist &&
            !isStoring)
        {
            if (canStore && !itemTemp.GetComponent<Item>().isCutted)
            {
                partnerBuilding.itemTransform = Instantiate(itemTransform, pointTransform.position, Quaternion.identity).transform;
                partnerBuilding.itemTemp = partnerBuilding.itemTransform.gameObject;
                partnerBuilding.itemTemp.GetComponent<Item>().UnMove();
            }

            isArrived = false;
            canRotate = false;
            SendItem();
            isReturned = true;
        }
    }

    /// <summary>
    /// 아이템을 발사하는 함수
    /// </summary>
    private void SendItem()
    {
        point.hitTransform.GetComponent<Point>().isItemExist = true;

        itemTemp.SetActive(true);
        itemTemp.GetComponent<Item>().CutSprites(isXType, canStore);
        itemTemp.GetComponent<Item>().PaintSprite();
        itemTransform = itemTemp.transform;
        itemTransform.GetComponent<Item>().ShowEffect();
        startPos = pointTransform;
        endPos = point.hitTransform;
        StartCoroutine(GetCenter(Vector3.up / (height * Vector3.Distance(startPos.position, endPos.position))));
        StartCoroutine(ThrowItem(itemTransform));
        StartCoroutine(WaitMoveForReturn());
    }
    #endregion
    #region Events
    private void Start()
    {
        InitSettings();
    }

    private void Update()
    {
        if (canStore)
            DirectStoreItem();

        DirectReturnItem();
    }

    private void LateUpdate()
    {
        LookCameraRotation();
    }
    #endregion
}
