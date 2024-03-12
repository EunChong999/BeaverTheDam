using System.Collections;
using UnityEngine;
using static UnityEditor.Progress;

public class Cutter : BasicBuilding
{
    #region Variables
    [Header("CutterBuilding")]

    [Space(10)]

    [SerializeField] float floatTime;
    [SerializeField] float arriveTime;
    [SerializeField] float spawnTime;
    [SerializeField] float height;
    [SerializeField] float speed;

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
    bool isArrived;
    bool isSpawned;
    bool isRemoved;

    #endregion
    #region Functions
    public override void InitSettings()
    {
        base.InitSettings();
        waitForArriveSeconds = new WaitForSeconds(arriveTime);
        waitForSpawnSeconds = new WaitForSeconds(spawnTime);
    }

    protected void DirectStoreItem()
    {
        if (pointingPoint != null && pointingPoint.hitTransform != null && pointingPoint.itemTransform != null)
        {
            if (pointingPoint.hitTransform.Equals(pointTransform) &&
                pointingPoint.canMove &&
                !isRemoved &&
                pointingPoint.isItemExist &&
                !pointingPoint.itemTransform.GetComponent<Item>().isMoving)
            {
                isArrived = false;
                itemTransform = pointingPoint.itemTransform;
                startPos = pointingPoint.transform.parent.GetComponent<BasicBuilding>().pointTransform;
                endPos = pointTransform;
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
        point.isItemExist = false;
        itemTemp = itemTransform.gameObject;
        itemTemp.SetActive(false);
        isRemoved = false;
    }

    /// <summary>
    /// 이동을 대기시키는 함수
    /// </summary>
    protected IEnumerator WaitMoveForReturn()
    {
        yield return waitForArriveSeconds;
        isArrived = true;
        yield return waitForSpawnSeconds;
        isSpawned = false;
    }

    protected void DirectReturnItem()
    {
        if (itemTemp != null &&
            !isRotating &&
            point.canMove &&
            !isSpawned &&
            !point.hitTransform.GetComponent<Point>().isItemExist)
        {
            itemTemp.SetActive(true);
            isArrived = false;
            SendItem();
            isSpawned = true;
        }
    }

    /// <summary>
    /// 아이템을 발사하는 함수
    /// </summary>
    private void SendItem()
    {
        itemTransform = itemTemp.transform;
        startPos = pointTransform;
        endPos = point.hitTransform;
        StartCoroutine(GetCenter(Vector3.up / (height * Vector3.Distance(startPos.position, endPos.position))));
        StartCoroutine(ThrowItem(itemTransform));
        StartCoroutine(WaitMoveForReturn());
    }
    #endregion
}
