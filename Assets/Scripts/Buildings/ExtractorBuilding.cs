using System.Collections;
using UnityEngine;

public class ExtractorBuilding : BasicBuilding
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
    /// 기본 설정들을 초기화하는 함수
    /// </summary>
    public override void InitSettings()
    {
        base.InitSettings();
        waitForArriveSeconds = new WaitForSeconds(arriveTime);
        waitForSpawnSeconds = new WaitForSeconds(spawnTime);
    }

    /// <summary>
    /// 발사에 대한 전체적인 동작을 지시하는 함수
    /// </summary>
    private void DirectSending()
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
    /// 아이템을 발사하는 함수
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
    protected IEnumerator WaitMove()
    {
        yield return waitForArriveSeconds;
        isArrived = true;
        yield return waitForSpawnSeconds;
        isSpawned = false;
    }

    /// <summary>
    /// 애니메이션을 재생하는 함수
    /// </summary>
    private void PlayAnimation()
    {
        animator.SetInteger("AngleInt", Mathf.RoundToInt(transform.eulerAngles.y));
        animator.SetFloat("AngleFloat", Mathf.RoundToInt(transform.eulerAngles.y));
    }
    #endregion
    #region Events
    private void OnMouseOver()
    {
        // 마우스 좌클릭
        if (Input.GetMouseButtonDown(0) && !isRotating)
        {
            DirectRotation(false, targetAngle, transform);
        }

        // 마우스 우클릭
        else if (Input.GetMouseButtonDown(1) && !isRotating)
        {
            DirectRotation(true, targetAngle, transform);
        }

        // 마우스 휠클릭
        else if (Input.GetMouseButtonDown(2) && !isRotating)
        {
            ChangeDirectionType();
        }
    }

    private void Start()
    {
        InitSettings();
    }

    private void Update()
    {
        DirectSending();
        PlayAnimation();
    }

    private void LateUpdate()
    {
        LookCameraRotation();
    }
    #endregion
}
