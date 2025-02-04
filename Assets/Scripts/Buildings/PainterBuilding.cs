using System.Collections;
using UnityEngine;
using UnityEngine.UIElements;

public class PainterBuilding : BasicBuilding, ISendableBuilding, IInputableBuilding, IOutputableBuilding
{
    #region Variables
    [Header("PainterBuilding")]

    [Space(10)]

    [HideInInspector] public Transform itemTransform;
    [HideInInspector] public Dye dye;

    [SerializeField] PainterBuilding partnerBuilding;
    [SerializeField] float floatTime;
    [SerializeField] float arriveTime;
    [SerializeField] float throwTime;
    [SerializeField] float spawnTime;
    [SerializeField] float returnTime;
    [SerializeField] float storeTime;
    [SerializeField] float sendTime;
    [SerializeField] float height;
    [SerializeField] float speed;
    [SerializeField] bool isInput;

    GameObject itemTemp;
    Transform startPos;
    Transform endPos;
    Transform hitTemp;
    SpriteRenderer itemSpriteRenderer;
    WaitForSeconds waitForArriveSeconds;
    WaitForSeconds waitForThrowSeconds;
    WaitForSeconds waitForReturnSeconds;
    WaitForSeconds waitForStoreSeconds;
    WaitForSeconds waitForSendSeconds;
    Vector3 centerPoint;
    Vector3 startRelCenter;
    Vector3 endRelCenter;
    readonly float startTime;
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
        waitForSendSeconds = new WaitForSeconds(sendTime);
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
                itemTemp == null &&
                !isRotating)
            {
                if (isInput == false)
                {
                    if (pointingPoint.itemTransform.CompareTag("Dye"))
                    {
                        isStoring = true;
                        isArrived = false;
                        canRotate = false;
                        itemTransform = pointingPoint.itemTransform;
                        dye = itemTransform.GetComponent<Dye>();
                        itemTemp = itemTransform.gameObject;
                        startPos = pointingPoint.transform.parent.GetComponent<BasicBuilding>().pointTransform;
                        endPos = pointTransform;
                        yield return waitForThrowSeconds;
                        StartCoroutine(GetCenter(Vector3.up / (height * Vector3.Distance(startPos.position, endPos.position))));
                        StartCoroutine(ThrowItem(itemTransform));
                        StartCoroutine(WaitForInput());
                        isRemoved = true;
                    }
                }
                else
                {
                    if (pointingPoint.itemTransform.CompareTag("Item"))
                    {
                        startPos = pointingPoint.transform.parent.GetComponent<BasicBuilding>().pointTransform;
                        endPos = pointTransform;
                        isStoring = true;
                        isArrived = false;
                        canRotate = false;
                        itemTransform = pointingPoint.itemTransform;
                        itemTemp = itemTransform.gameObject;
                        yield return waitForThrowSeconds;
                        StartCoroutine(GetCenter(Vector3.up / (height * Vector3.Distance(startPos.position, endPos.position))));
                        StartCoroutine(ThrowItem(itemTransform));
                        StartCoroutine(WaitForInput());
                        isRemoved = true;
                    }
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

        if (itemTransform != null)
            itemSpriteRenderer = itemTransform.GetComponent<Item>().spriteRenderer;

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
        yield return waitForArriveSeconds;
        isArrived = true;
        canRotate = true;
        yield return waitForReturnSeconds;

        if (isInput == false)
        {
            partnerBuilding.itemTemp = null;
            partnerBuilding.point.itemTransform = null;
        }
        
        isReturned = false;
        itemTemp = null;
    }

    public void Output()
    {
        if (itemTemp != null &&
            !isRotating &&
            point.canMove &&
            !isReturned &&
            !point.hitTransform.GetComponent<Point>().isItemExist &&
            !isStoring &&
            partnerBuilding.itemTemp != null &&
            isInput == false)
        {
            hitTemp = point.hitTransform;
            isArrived = false;
            canRotate = false;
            StartCoroutine(DirectSending());
            isReturned = true;
        }
    }

    /// <summary>
    /// 아이템을 발사하는 함수
    /// </summary>
    IEnumerator DirectSending()
    {
        yield return waitForSendSeconds;

        ReleaseStoreItemImg();
        partnerBuilding.ReleaseStoreItemImg();

        if (point.hitTransform != null && point.hitTransform == hitTemp)
        {
            point.hitTransform.GetComponent<Point>().isItemExist = true;
            itemTemp = partnerBuilding.itemTemp;
            itemTemp.SetActive(true);

            if (dye != null)
                itemTemp.GetComponent<Item>().PaintSprite(dye.myColor, dye.myColorType);

            itemTransform = itemTemp.transform;
            itemTransform.GetComponent<Item>().ShowEffect(true);
            startPos = pointTransform;
            endPos = point.hitTransform;
            StartCoroutine(GetCenter(Vector3.up / (height * Vector3.Distance(startPos.position, endPos.position))));
            StartCoroutine(ThrowItem(itemTransform));
            StartCoroutine(WaitForOutput());
        }
        else
        {
            StartCoroutine(DirectSending());
        }
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
        StartCoroutine(Input());
        Output();
    }

    private void LateUpdate()
    {
        LookCameraRotation();
    }
    #endregion
}
