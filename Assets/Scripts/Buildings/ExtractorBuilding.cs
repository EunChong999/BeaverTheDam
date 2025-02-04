using System.Collections;
using UnityEngine;

public class ExtractorBuilding : BasicBuilding, ISendableBuilding, IOutputableBuilding
{
    #region Variabless

    [Header("ExtractorBuilding")]

    [Space(10)]

    [SerializeField] GameObject item;
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
    SpriteRenderer itemSpriteRenderer;
    WaitForSeconds waitForArriveSeconds;
    WaitForSeconds waitForSpawnSeconds;
    bool isArrived;
    bool isSpawned;

    #endregion
    #region Functions

    public override void InitSettings()
    {
        base.InitSettings();
        waitForArriveSeconds = new WaitForSeconds(arriveTime);
        waitForSpawnSeconds = new WaitForSeconds(spawnTime);
        itemSpriteRenderer = item.GetComponent<Item>().spriteRenderer;
        ApplyStoreItemImg(itemSpriteRenderer);
    }

    public void Output()
    {
        if (!isRotating &&
            point.canMove &&
            !isSpawned &&
            !point.hitTransform.GetComponent<Point>().isItemExist &&
            point.hitTransform.GetComponent<Point>().transform.parent.GetComponent<BasicBuilding>().buildingType == buildingType.movableType &&
            ObjectPooler.Instance.canSpawn)
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
        point.hitTransform.GetComponent<Point>().isItemExist = true;

        startPos = pointTransform;
        endPos = point.hitTransform;
        animator.SetTrigger("Spawn");
        itemTransform = ObjectPooler.Instance.SpawnFromPool(item.name, pointTransform.position, Quaternion.identity).transform;
        itemTransform.GetComponent<Item>().ShowEffect(true);
        StartCoroutine(GetCenter(Vector3.up / (height * Vector3.Distance(startPos.position, endPos.position))));
        StartCoroutine(ThrowItem(itemTransform));
        StartCoroutine(WaitForOutput());
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

    public IEnumerator WaitForOutput()
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
        if (!CanRotation())
            return;

        if (isRotating)
            return;

        // 마우스 우클릭
        if (Input.GetMouseButtonDown(0))
        {
            DirectRotation(false, targetAngle, transform, true);
        }

        // 마우스 좌클릭 
        else if (Input.GetMouseButtonDown(1))
        {
            DirectRotation(true, targetAngle, transform, true);
        }

        // 마우스 휠클릭
        else if (Input.GetMouseButtonDown(2))
        {
            ChangeDirectionType(true);
        }
    }

    protected override void Start()
    {
        base.Start();
        InitSettings();
    }

    private void Update()
    {
        Output();
        PlayAnimation();
    }

    private void LateUpdate()
    {
        LookCameraRotation();
    }
    #endregion
}
