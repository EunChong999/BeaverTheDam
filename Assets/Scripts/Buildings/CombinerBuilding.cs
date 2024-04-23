using System.Collections;
using UnityEngine;

public class CombinerBuilding : BasicBuilding, ISendableBuilding, IInputableBuilding, IOutputableBuilding
{
    #region Variables
    [Header("MixerBuilding")]

    [Space(10)]

    [SerializeField] CombinerBuilding partnerBuilding;
    [SerializeField] float floatTime;
    [SerializeField] float arriveTime;
    [SerializeField] float throwTime;
    [SerializeField] float spawnTime;
    [SerializeField] float returnTime;
    [SerializeField] float storeTime;
    [SerializeField] float height;
    [SerializeField] float speed;
    [SerializeField] bool isXType;
    [SerializeField] bool isInput;

    GameObject itemTemp;
    Transform startPos;
    Transform endPos;
    SpriteRenderer itemSpriteRenderer;
    WaitForSeconds waitForArriveSeconds;
    WaitForSeconds waitForThrowSeconds;
    WaitForSeconds waitForReturnSeconds;
    WaitForSeconds waitForStoreSeconds;
    Vector3 centerPoint;
    Vector3 startRelCenter;
    Vector3 endRelCenter;
    float startTime;
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
        waitForThrowSeconds = new WaitForSeconds(throwTime);
        waitForReturnSeconds = new WaitForSeconds(returnTime);
        waitForStoreSeconds = new WaitForSeconds(storeTime);
    }

    public IEnumerator Input()
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
                if (pointingPoint.itemTransform.CompareTag("Item"))
                {
                    startPos = pointingPoint.transform.parent.GetComponent<BasicBuilding>().pointTransform;
                    endPos = pointTransform;
                    isStoring = true;
                    isArrived = false;
                    canRotate = false;
                    itemTemp = pointingPoint.itemTransform.gameObject;
                    yield return waitForThrowSeconds;
                    StartCoroutine(GetCenter(Vector3.up / (height * Vector3.Distance(startPos.position, endPos.position))));
                    StartCoroutine(ThrowItem(itemTemp.transform));
                    StartCoroutine(WaitForInput());
                    isRemoved = true;
                }
            }
        }
    }

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

    public IEnumerator WaitForInput()
    {
        yield return waitForArriveSeconds;

        itemSpriteRenderer = itemTemp.GetComponent<Item>().spriteRenderer;
        ApplyStoreItemImg(itemSpriteRenderer);

        isArrived = true;
        canRotate = true;
        itemTemp.SetActive(false);
        point.isItemExist = false;
        isRemoved = false;
        yield return waitForStoreSeconds;
        isStoring = false;
    }

    public IEnumerator WaitForOutput()
    {
        itemSpriteRenderer = null;
        partnerBuilding.ApplyStoreItemImg(itemSpriteRenderer);

        yield return waitForArriveSeconds;
        isArrived = true;
        canRotate = true;
        yield return waitForReturnSeconds;

        Destroy(partnerBuilding.itemTemp);

        isReturned = false;
        itemTemp = null;
    }

    public void Output()
    {
        if (itemTemp != null &&
            partnerBuilding.itemTemp != null &&
            !isRotating &&
            point.canMove &&
            !isReturned &&
            !point.hitTransform.GetComponent<Point>().isItemExist &&
            !isStoring &&
            !partnerBuilding.isStoring)
        {
            isArrived = false;
            canRotate = false;
            DirectSending();
            isReturned = true;
        }
    }

    /// <summary>
    /// 아이템을 보내는 함수
    /// </summary>
    private void DirectSending()
    {
        ReleaseStoreItemImg();

        point.hitTransform.GetComponent<Point>().isItemExist = true;

        itemTemp.SetActive(true);
        itemTemp.GetComponent<Item>().ShowEffect(true);
        startPos = pointTransform;
        endPos = point.hitTransform;
        StartCoroutine(GetCenter(Vector3.up / (height * Vector3.Distance(startPos.position, endPos.position))));
        StartCoroutine(ThrowItem(itemTemp.transform));
        StartCoroutine(WaitForOutput());
    }
    #endregion
    #region Events
    private void Start()
    {
        InitSettings();
    }

    private void Update()
    {
        StartCoroutine(Input());

        if (isInput == false)
            Output();
    }

    private void LateUpdate()
    {
        LookCameraRotation();
    }
    #endregion
}
