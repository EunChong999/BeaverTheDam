using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum painterType
{
    inputType,
    outputType
}

public class Painter : BasicBuilding
{
    #region Variables
    [Header("PainterBuilding")]

    [Space(10)]

    [SerializeField] Painter partnerPainter;
    [SerializeField] protected painterType painterType;
    [SerializeField] float floatTime;
    [SerializeField] float arriveTime;
    [SerializeField] float spawnTime;
    [SerializeField] float returnTime;
    [SerializeField] float storeTime;
    [SerializeField] float sendTime;
    [SerializeField] float height;
    [SerializeField] float speed;

    float startTime;
    Vector3 centerPoint;
    Vector3 startRelCenter;
    Vector3 endRelCenter;
    public Transform itemTransform;
    Transform startPos;
    Transform endPos;
    GameObject itemTemp;
    WaitForSeconds waitForArriveSeconds;
    WaitForSeconds waitForReturnSeconds;
    WaitForSeconds waitForStoreSeconds;
    WaitForSeconds waitForSendSeconds;
    bool isArrived;
    bool isReturned;
    bool isRemoved;
    bool isStoring;
    Transform hitTemp;
    Color colorTemp;

    #endregion
    #region Functions
    public override void InitSettings()
    {
        base.InitSettings();
        waitForArriveSeconds = new WaitForSeconds(arriveTime);
        waitForReturnSeconds = new WaitForSeconds(returnTime);
        waitForStoreSeconds = new WaitForSeconds(storeTime);
        waitForSendSeconds = new WaitForSeconds(sendTime);
    }

    protected void DirectStoreItem()
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
                if (painterType == painterType.outputType) 
                {
                    if (pointingPoint.itemTransform.CompareTag("Dye"))
                    {
                        isStoring = true;
                        isArrived = false;
                        canRotate = false;
                        itemTransform = pointingPoint.itemTransform;
                        itemTemp = itemTransform.gameObject;
                        startPos = pointingPoint.transform.parent.GetComponent<BasicBuilding>().pointTransform;
                        endPos = pointTransform;
                        StartCoroutine(GetCenter(Vector3.up / (height * Vector3.Distance(startPos.position, endPos.position))));
                        StartCoroutine(ThrowItem(itemTransform));
                        StartCoroutine(WaitMoveForStore());
                        pointingPoint.Exit();
                        isRemoved = true;
                    }
                }
                else
                {
                    if (pointingPoint.itemTransform.CompareTag("Item"))
                    {
                        isStoring = true;
                        isArrived = false;
                        canRotate = false;
                        itemTransform = pointingPoint.itemTransform;
                        itemTemp = itemTransform.gameObject;
                        startPos = pointingPoint.transform.parent.GetComponent<BasicBuilding>().pointTransform;
                        endPos = pointTransform;
                        StartCoroutine(GetCenter(Vector3.up / (height * Vector3.Distance(startPos.position, endPos.position))));
                        StartCoroutine(ThrowItem(itemTransform));
                        StartCoroutine(WaitMoveForStore());
                        pointingPoint.Exit();
                        isRemoved = true;
                    }
                }
            }
        }
    }

    /// <summary>
    /// 포물선의 중앙을 결정하는 함수
    /// </summary>
    protected IEnumerator GetCenter(Vector3 direction)
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
    protected IEnumerator ThrowItem(Transform item)
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
    protected IEnumerator WaitMoveForStore()
    {
        yield return waitForArriveSeconds;
        isArrived = true;
        canRotate = true;
        itemTemp.SetActive(false);
        point.isItemExist = false;
        isRemoved = false;

        point.hitTransform.GetComponent<Point>().Enter(itemTransform);

        yield return waitForStoreSeconds;
        isStoring = false;
    }

    /// <summary>
    /// 이동을 대기시키는 함수
    /// </summary>
    protected IEnumerator WaitMoveForReturn()
    {
        yield return waitForArriveSeconds;
        isArrived = true;
        canRotate = true;
        yield return waitForReturnSeconds;
        isReturned = false;
        itemTemp = null;

        if (painterType == painterType.outputType) 
        {
            partnerPainter.itemTemp = null;
            partnerPainter.point.itemTransform = null;
        }
    }

    protected void DirectReturnItem()
    {
        if (itemTemp != null &&
            !isRotating &&
            point.canMove &&
            !isReturned &&
            !point.hitTransform.GetComponent<Point>().isItemExist &&
            !isStoring && 
            partnerPainter.itemTemp != null &&
            painterType == painterType.outputType)
        {
            hitTemp = point.hitTransform;
            isArrived = false;
            canRotate = false;
            StartCoroutine(SendItem());
            isReturned = true;
        }
    }

    /// <summary>
    /// 아이템을 발사하는 함수
    /// </summary>
    IEnumerator SendItem()
    {
        yield return waitForSendSeconds;

        if (point.hitTransform != null && point.hitTransform == hitTemp)
        {
            colorTemp = itemTransform.GetComponent<Item>().spriteRenderer.color;
            itemTemp = partnerPainter.itemTemp;
            itemTemp.SetActive(true);
            itemTransform = itemTemp.transform;
            itemTemp.GetComponent<Item>().spriteRenderer.sprite = itemTemp.GetComponent<Item>().replaceSprite;
            itemTransform.GetComponent<Item>().spriteRenderer.color = colorTemp;
            itemTransform.GetComponent<Item>().ShowEffect();
            startPos = pointTransform;
            endPos = point.hitTransform;
            StartCoroutine(GetCenter(Vector3.up / (height * Vector3.Distance(startPos.position, endPos.position))));
            StartCoroutine(ThrowItem(itemTransform));
            StartCoroutine(WaitMoveForReturn());
        }
        else
        {
            StartCoroutine(SendItem());
        }
    }
    #endregion
}
