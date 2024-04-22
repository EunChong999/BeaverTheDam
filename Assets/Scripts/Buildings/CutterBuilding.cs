using System.Collections;
using UnityEngine;

public class CutterBuilding : BasicBuilding, ISendableBuilding, IInputableBuilding, IOutputableBuilding
{
    #region Variables
    [Header("CutterBuilding")]

    [Space(10)]

    [SerializeField] CutterBuilding partnerBuilding;
    [SerializeField] float floatTime;
    [SerializeField] float arriveTime;
    [SerializeField] float spawnTime;
    [SerializeField] float returnTime;
    [SerializeField] float storeTime;
    [SerializeField] float height;
    [SerializeField] float speed;
    [SerializeField] bool isXType;
    [SerializeField] bool isInput;

    DoubleBasicBuilding doubleBasicBuilding;
    GameObject itemTemp;
    Transform itemTransform;
    Transform startPos;
    Transform endPos;
    SpriteRenderer itemSpriteRenderer;
    WaitForSeconds waitForArriveSeconds;
    WaitForSeconds waitForSpawnSeconds;
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
        waitForSpawnSeconds = new WaitForSeconds(spawnTime);
        waitForReturnSeconds = new WaitForSeconds(returnTime);
        waitForStoreSeconds = new WaitForSeconds(storeTime);
        doubleBasicBuilding = transform.parent.GetComponent<DoubleBasicBuilding>();
    }

    public void Input()
    {
        if (pointingPoint != null && pointingPoint.hitTransform != null && pointingPoint.itemTransform != null)
        {
            if (pointingPoint.hitTransform.Equals(pointTransform) &&
                pointingPoint.canMove &&
                !isRemoved &&
                pointingPoint.isItemExist &&
                !pointingPoint.itemTransform.GetComponent<Item>().isMoving &&
                itemTemp == null && 
                partnerBuilding.itemTemp == null)
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
                    StartCoroutine(GetCenter(Vector3.up / (height * Vector3.Distance(startPos.position, endPos.position))));
                    StartCoroutine(ThrowItem(itemTransform));
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

        if (itemTransform.GetComponent<Item>().isCutted)
        {
            itemSpriteRenderer = itemTransform.GetComponent<Item>().spriteRenderer;
            ApplyStoreItemImg(itemSpriteRenderer);
        }
        else
        {
            itemSpriteRenderer = itemTransform.GetComponent<Item>().ApplyCutSprite(isXType, isInput, doubleBasicBuilding.isReversed);
            ApplyStoreItemImg(itemSpriteRenderer);

            itemSpriteRenderer = itemTransform.GetComponent<Item>().ApplyCutSprite(partnerBuilding.isXType, partnerBuilding.isInput, doubleBasicBuilding.isReversed);
            partnerBuilding.ApplyStoreItemImg(itemSpriteRenderer);
        }

        isArrived = true;
        canRotate = true;
        itemTemp.SetActive(false);
        point.isItemExist = false;
        isRemoved = false;
        yield return waitForStoreSeconds;

        if (isInput == true && !itemTemp.GetComponent<Item>().isCutted)
        {
            partnerBuilding.itemTransform = Instantiate(itemTransform, pointTransform.position, Quaternion.identity).transform;
            partnerBuilding.itemTemp = partnerBuilding.itemTransform.gameObject;
            partnerBuilding.itemTemp.GetComponent<Item>().UnMove();
        }

        isStoring = false;
    }

    public IEnumerator WaitForOutput()
    {
        yield return waitForArriveSeconds;
        isArrived = true;
        canRotate = true;
        yield return waitForReturnSeconds;
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
            !isStoring)
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
        itemTemp.GetComponent<Item>().CutSprite(isXType, isInput, doubleBasicBuilding.isReversed);
        itemTemp.GetComponent<Item>().shadow.CutSprite(isXType, isInput, doubleBasicBuilding.isReversed);
        itemTransform = itemTemp.transform;
        itemTransform.GetComponent<Item>().ShowEffect(true);
        startPos = pointTransform;
        endPos = point.hitTransform;
        StartCoroutine(GetCenter(Vector3.up / (height * Vector3.Distance(startPos.position, endPos.position))));
        StartCoroutine(ThrowItem(itemTransform));
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
        if (isInput == true)
            Input();

        Output();
    }

    private void LateUpdate()
    {
        LookCameraRotation();
    }
    #endregion
}
