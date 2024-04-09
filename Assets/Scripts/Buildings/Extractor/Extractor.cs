using System.Collections;
using UnityEngine;

public class Extractor : BasicBuilding
{
    #region Variables

    [Header("ExtractorBuilding")]

    [Space(10)]

    [SerializeField] float floatTime;
    [SerializeField] float arriveTime;
    [SerializeField] float spawnTime;
    [SerializeField] float height;
    [SerializeField] float speed;
    [SerializeField] GameObject item;
    [SerializeField] Animator animator;

    float startTime;
    Vector3 centerPoint;
    Vector3 startRelCenter;
    Vector3 endRelCenter;
    Transform itemTransform;
    Transform startPos;
    Transform endPos;
    WaitForSeconds waitForArriveSeconds;
    WaitForSeconds waitForSpawnSeconds;
    bool isArrived;
    bool isSpawned;

    #endregion
    #region Functions
    /// <summary>
    /// �⺻ �������� �ʱ�ȭ�ϴ� �Լ�
    /// </summary>
    public override void InitSettings()
    {
        base.InitSettings();
        waitForArriveSeconds = new WaitForSeconds(arriveTime);
        waitForSpawnSeconds = new WaitForSeconds(spawnTime);
    }

    /// <summary>
    /// �߻翡 ���� ��ü���� ������ �����ϴ� �Լ�
    /// </summary>
    protected void DirectSending()
    {
        if (!isRotating &&
            point.canMove &&
            !isSpawned &&
            !point.hitTransform.GetComponent<Point>().isItemExist)
        {
            isArrived = false;
            SendItem();
            isSpawned = true;
        }
    }

    /// <summary>
    /// �������� �߻��ϴ� �Լ�
    /// </summary>
    private void SendItem()
    {
        startPos = pointTransform;
        endPos = point.hitTransform;
        animator.SetTrigger("Spawn");
        itemTransform = Instantiate(item, pointTransform.position, Quaternion.identity).transform;
        itemTransform.GetComponent<Item>().ShowEffect();
        StartCoroutine(GetCenter(Vector3.up / (height * Vector3.Distance(startPos.position, endPos.position))));
        StartCoroutine(ThrowItem(itemTransform));
        StartCoroutine(WaitMove());
    }

    /// <summary>
    /// �������� �߾��� �����ϴ� �Լ�
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
    /// �������� �߻��ϴ� �Լ�
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
    /// �̵��� ����Ű�� �Լ�
    /// </summary>
    protected IEnumerator WaitMove()
    {
        yield return waitForArriveSeconds;
        isArrived = true;
        yield return waitForSpawnSeconds;
        isSpawned = false;
    }
    #endregion
}
