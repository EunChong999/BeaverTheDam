using DG.Tweening;
using System.Collections;
using UnityEngine;

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
    Coroutine coroutine;

    private void Start()
    {
        canPlay = true;
    }

    void Update()
    {
        moveSpeed = BuildingManager.instance.speed;
        originScale = BuildingManager.instance.originScale;

        CheckCanMove();
    }

    public void CheckCanMove()
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

            if (detector.canMove && !hitInfo.transform.GetChild(1).GetComponent<Point>().isItemExist)
            {
                canMove = true;
                Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.right) * hitInfo.distance, Color.red);
                hitTransform = hitInfo.transform.GetChild(1);
                hitTransform.parent.GetComponent<BasicBuilding>().pointingPoint = GetComponent<Point>();
            }
            else
            {
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

    public void DoMove(Collider obj)
    {
        if (!obj.GetComponent<Item>().isMoving)
        {
            canPlay = false;
        }
        else
        {
            canPlay = true;
        }

        isItemExist = true;
        itemTransform = obj.transform;

        if (hitTransform != null && !obj.GetComponent<Item>().isMoving && canMove && isMovable)
        {
            coroutine = StartCoroutine(CarryItem(itemTransform, hitTransform));
            itemTransform.GetComponent<Item>().EnMove();
        }
    }

    public void DoneMove(Transform transform)
    {
        canPlay = true;
        isItemExist = false;
        itemTransform = null;
    }

    private void OnTriggerExit(Collider obj)
    {
        if (obj.CompareTag("Item") || obj.CompareTag("Dye"))
        {
            DoneMove(obj.transform);
        }
    }

    private void OnTriggerStay(Collider obj)
    {
        if (obj.CompareTag("Item") || obj.CompareTag("Dye"))
        {
            DoMove(obj);
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
