using DG.Tweening;
using System.Collections;
using UnityEngine;

public class Point : MonoBehaviour
{
    [SerializeField] Detector detector;
    [SerializeField] LayerMask layerMask;
    [SerializeField] float maxDistance;

    public Vector3 originScale;

    public bool isMovable;
    public bool canMove { get; private set; }
    public bool canPlay { get; private set; }
    public bool isItemExist;
    public bool isStopped;

    public Transform itemTransform;
    public Transform hitTransform;
    float moveSpeed;

    private void Start()
    {
        maxDistance = BuildingManager.instance.maxDistance;
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
                hitTransform.parent.GetComponent<BasicBuilding>().pointingPoint = this;
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

    public void DoMove(Transform transform)
    {
        if (!transform.GetComponent<Item>().isMoving)
        {
            canPlay = false;
        }
        else
        {
            canPlay = true;
        }

        isItemExist = true;
        itemTransform = transform;

        if (hitTransform != null && !transform.GetComponent<Item>().isMoving && canMove && isMovable)
        {
            StartCoroutine(CarryItem(itemTransform, hitTransform));
            itemTransform.GetComponent<Item>().EnMove();

            if (this.transform.parent.GetComponent<BasicBuilding>().buildingType == buildingType.fixedType)
                return;

            hitTransform.GetComponent<Point>().isItemExist = true;
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
        if (!obj.CompareTag("Item") && !obj.CompareTag("Dye"))
            return;

        DoneMove(obj.transform);
    }

    private void OnTriggerStay(Collider obj)
    {
        if (!obj.CompareTag("Item") && !obj.CompareTag("Dye"))
            return;

        DoMove(obj.transform);
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
}
