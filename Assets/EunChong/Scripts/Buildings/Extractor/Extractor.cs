using System.Collections;
using UnityEngine;

public class Extractor : BasicBuilding
{
    #region Variables
    public Transform itemTransform;
    public Transform startPos;
    public Transform endPos;
    public float journeyTime = 1.0f;
    public float speed;
    public bool repeatable;

    float startTime;
    Vector3 centerPoint;
    Vector3 startRelCenter;
    Vector3 endRelCenter;

    bool isFinished;

    #endregion
    #region Functions
    protected void SendItem()
    {
        StartCoroutine(GetCenter(Vector3.up / (10 * Vector3.Distance(startPos.position, endPos.position))));
        StartCoroutine(ThrowItem());
        StartCoroutine(test());
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="direction">
    /// 
    /// </param>
    protected IEnumerator GetCenter(Vector3 direction)
    {
        while (!isFinished)
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
    protected IEnumerator ThrowItem()
    {
        while (!isFinished)
        {
            float fracComplete = (Time.time - startTime) / journeyTime * speed;
            itemTransform.position = Vector3.Slerp(startRelCenter, endRelCenter, fracComplete * speed);
            itemTransform.position += centerPoint;
            yield return null;
        }
    }

    protected IEnumerator test()
    {
        while (!itemTransform.GetComponent<Item>().isMoving)
        {
            isFinished = false;
            yield return null;
        }

        yield return new WaitForSeconds(0.125f);

        isFinished = true;
    }
    #endregion
}
