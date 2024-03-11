using System.Collections;
using UnityEngine;

public class Trash : BasicBuilding
{
    #region Variables

    [Header("TrashBuilding")]

    [Space(10)]

    [SerializeField] float floatTime;
    [SerializeField] float arriveTime;
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
    bool isArrived;
    bool isRemoved;

    #endregion
    #region Functions
    /// <summary>
    /// 기본 설정들을 초기화하는 함수
    /// </summary>
    public override void InitSettings()
    {
        base.InitSettings();
        waitForArriveSeconds = new WaitForSeconds(arriveTime);
    }

    /// <summary>
    /// 아이템을 발사하는 함수
    /// </summary>
    protected void RemoveItem()
    {
        if (point.hitTransform != null && point.hitTransform.GetComponent<Point>().itemTransform != null)
        {
            if (point.diffDir &&
                point.canMove &&
                !isRemoved &&
                point.hitTransform.GetComponent<Point>().isItemExist &&
                !point.hitTransform.GetComponent<Point>().itemTransform.GetComponent<Item>().isMoving)
            {
                isArrived = false;
                itemTransform = point.hitTransform.GetComponent<Point>().itemTransform;
                startPos = point.hitTransform;
                endPos = pointTransform;
                StartCoroutine(GetCenter(Vector3.up / (height * Vector3.Distance(startPos.position, endPos.position))));
                StartCoroutine(ThrowItem(itemTransform));
                StartCoroutine(WaitMove());
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
    protected IEnumerator WaitMove()
    {
        yield return waitForArriveSeconds;
        isArrived = true;
        Destroy(itemTransform.gameObject);
        isRemoved = false;
    }
    #endregion
}
