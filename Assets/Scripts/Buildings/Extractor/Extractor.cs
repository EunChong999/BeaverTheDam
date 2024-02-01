using System.Collections;
using UnityEngine;

public class Extractor : BasicBuilding
{
    #region Variables
    public float journeyTime;
    public float delayTime;
    public float height;
    public float speed;
    public bool repeatable;

    float startTime;
    Vector3 centerPoint;
    Vector3 startRelCenter;
    Vector3 endRelCenter;
    Transform itemTransform;
    Transform startPos;
    Transform endPos;
    WaitForSeconds waitForSeconds;

    bool isFinished;
    bool isHitted;

    #endregion
    #region Functions
    /// <summary>
    /// �⺻ �������� �ʱ�ȭ�ϴ� �Լ�
    /// </summary>
    public override void InitSettings()
    {
        base.InitSettings();

        waitForSeconds = new WaitForSeconds(delayTime);
    }

    /// <summary>
    /// �������� �߻��ϴ� �Լ�
    /// </summary>
    protected void SendItem()
    {
        if (point.canMove)
        {
            itemTransform = point.itemTransform;
            startPos = pointTransform;
            endPos = point.hitTransform;
            isHitted = true;
        }

        if (isHitted)
        {
            StartCoroutine(GetCenter(Vector3.up / (height * Vector3.Distance(startPos.position, endPos.position))));
            StartCoroutine(ThrowItem(itemTransform));
            StartCoroutine(WaitMove());
            isHitted = false;
        }
    }

    /// <summary>
    /// �������� �߾��� �����ϴ� �Լ�
    /// </summary>
    /// <param name="direction">
    /// �������� ����
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
    /// �������� �߻��ϴ� �Լ�
    /// </summary>
    protected IEnumerator ThrowItem(Transform item)
    {
        while (!isFinished && item != null)
        {
            float fracComplete = (Time.time - startTime) / journeyTime * speed;
            item.position = Vector3.Slerp(startRelCenter, endRelCenter, fracComplete * speed);
            item.position += centerPoint;
            yield return null;
        }
    }

    /// <summary>
    /// �̵��� ����Ű�� �Լ�
    /// </summary>
    protected IEnumerator WaitMove()
    {
        yield return waitForSeconds;

        isFinished = true;
    }
    #endregion
}
