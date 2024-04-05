using DG.Tweening;
using System.Collections;
using UnityEngine;
using static UnityEditor.Progress;

public class Point : MonoBehaviour
{
    [SerializeField] Detector detector;
    [SerializeField] LayerMask layerMask;
    [SerializeField] float maxDistance;
    [SerializeField] float startScaleTime;
    [SerializeField] float endScaleTime;
    [SerializeField] Ease startScaleEase;
    [SerializeField] Ease endScaleEase;

    public Vector3 originScale;

    public bool isMovable;
    public bool canMove { get; private set; }
    public bool canPlay { get; private set; }
    public bool isItemExist;
    public bool isStopped;

    public Transform itemTransform;
    public Transform hitTransform;
    Sequence itemScaleSequence;
    float moveSpeed;
    bool firstContect;

    private void Start()
    {
        canPlay = true;
    }

    void Update()
    {
        moveSpeed = BuildingManager.instance.speed;
        originScale = BuildingManager.instance.originScale;

        CanMove();
    }

    public void CanMove()
    {
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.right), out RaycastHit hitInfo, maxDistance, layerMask) && !transform.parent.GetComponent<BasicBuilding>().isRotating)
        {
            if (hitInfo.transform.GetComponent<BasicBuilding>().buildingType == buildingType.movableType)
            {
                isMovable = true;
            }
            else
            {
                isMovable = false;
            }

            // 바라보는 건물에 아이템이 존재하지 않을 때
            if (detector.canMove && !hitInfo.transform.GetChild(1).GetComponent<Point>().isItemExist)
            {
                if (!canMove)
                {
                    Debug.Log("이동 가능");
                    firstContect = true;
                }

                canMove = true;
                Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.right) * hitInfo.distance, Color.red);
                hitTransform = hitInfo.transform.GetChild(1);
                hitTransform.parent.GetComponent<BasicBuilding>().pointingPoint = GetComponent<Point>();

                if (isStopped)
                {
                    isStopped = false;
                }
            }
            else
            {
                if (canMove)
                {
                    Debug.Log("이동 불가능");
                    firstContect = false;
                }

                canMove = false;
                Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.right) * maxDistance, Color.green);

                if (hitTransform != null)
                    hitTransform.parent.GetComponent<BasicBuilding>().pointingPoint = null;

                hitTransform = null;
            }

            if (!isItemExist)
            {
                canPlay = true;
            }
        }
        else
        {
            canMove = false;
            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.right) * maxDistance, Color.green);

            if (hitTransform != null)
                hitTransform.parent.GetComponent<BasicBuilding>().pointingPoint = null;

            hitTransform = null;
            canPlay = false;
        }
    }

    private void OnTriggerExit(Collider obj)
    {
        if (obj.CompareTag("Item") || obj.CompareTag("Dye"))
        {
            canPlay = true;
        }
    }

    private void OnTriggerStay(Collider obj)
    {
        if (obj.CompareTag("Item") || obj.CompareTag("Dye"))
        {
            if (!obj.GetComponent<Item>().isMoving)
            {
                canPlay = false;
            }
            else
            {
                canPlay = true;
            }
        }
    }

    public void Exit()
    {
        isItemExist = false;
        itemTransform = null;
    }

    public void Enter(Transform item)
    {
        isItemExist = true;
        itemTransform = item.transform;
    }

    public void Move(Transform item)
    {
        if (isItemExist && hitTransform != null && !item.GetComponent<Item>().isMoving && canMove && isMovable)
        {
            StartCoroutine(CarryItem(itemTransform, hitTransform));
            itemTransform.GetComponent<Item>().EnMove();
        }
        else
        {
            isStopped = true;
        }
    }

    /// <summary>
    /// 물건을 운반하는 함수
    /// </summary>
    public IEnumerator CarryItem(Transform itemTransform, Transform hitTransform)
    {
        float threshold = 0.01f;

        while (itemTransform != null && Vector3.Distance(itemTransform.position, hitTransform.position) > threshold)
        {
            itemTransform.position = Vector3.MoveTowards(itemTransform.position, hitTransform.position, Time.deltaTime * moveSpeed);
            yield return null;
        }

        if (itemTransform != null)
        {
            itemTransform.position = hitTransform.position;
            itemTransform.GetComponent<Item>().UnMove();
        }

        Exit();
        hitTransform.GetComponent<Point>().Enter(itemTransform);
        hitTransform.GetComponent<Point>().Move(itemTransform);
    }

    /// <summary>
    /// 회전시 트위닝 효과를 주는 함수
    /// </summary>
    public void ShowEffect()
    {
        if (itemTransform != null)
        {
            itemScaleSequence = DOTween.Sequence().SetAutoKill(true)
            .Append(itemTransform.GetComponent<Item>().spriteTransform.DOScale(new Vector3(
                itemTransform.GetComponent<Item>().spriteTransform.localScale.x / 1.5f,
                itemTransform.GetComponent<Item>().spriteTransform.localScale.y / 1.25f,
                itemTransform.GetComponent<Item>().spriteTransform.localScale.z / 1.5f),
                startScaleTime).SetEase(startScaleEase))
            .Append(itemTransform.GetComponent<Item>().spriteTransform.DOScale(originScale, endScaleTime).SetEase(endScaleEase));
        }
    }
}
