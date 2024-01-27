using System.Collections;
using UnityEngine;

public class Point : MonoBehaviour
{
    [SerializeField] LayerMask layerMask;
    [SerializeField] float moveSpeed;
    [SerializeField] float maxDistance;

    public bool canMove { get; private set; }
    public bool canPlay { get; private set; }
    public bool isItemExist { get; private set; }

    Transform itemTransform;
    Transform hitTransform;

    void Update()
    {
        CanMove();
    }

    public void CanMove()
    {
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.right), out RaycastHit hitInfo, maxDistance, layerMask))
        {
            bool IsSameDir()
            {
                bool dir =
                    // 이동형이 직선형일 때, 건물끼리 바라보는 방향이 같은 경우
                    ((hitInfo.transform.GetComponent<ConveyorBeltBuilding>().moveType == moveType.straight &&
                    (int)hitInfo.transform.eulerAngles.y == (int)transform.parent.eulerAngles.y) ||

                    // 이동형이 곡선형일 때, 바라보는 건물이 해당 건물보다 방향이 90도 돌아가 있는 경우 
                    (hitInfo.transform.GetComponent<ConveyorBeltBuilding>().moveType == moveType.curve &&
                    (int)hitInfo.transform.eulerAngles.y == (int)(transform.parent.eulerAngles.y) + 90) ||

                    // 이동형이 곡선형일 때, 바라보는 건물의 방향이 0도이고, 해당 건물이 바라보는 건물보다 270도 돌아가 있는 경우
                    hitInfo.transform.GetComponent<ConveyorBeltBuilding>().moveType == moveType.curve &&
                    (int)hitInfo.transform.eulerAngles.y == 0 &&
                    (int)hitInfo.transform.eulerAngles.y == (int)(transform.parent.eulerAngles.y) - 270);

                return dir;
            }

            // 바라보는 건물에 아이템이 존재하지 않을 때
            if (IsSameDir() && !hitInfo.transform.GetComponent<ConveyorBeltBuilding>().isItemExist)
            {
                canMove = true;
                Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.right) * hitInfo.distance, Color.red);
                hitTransform = hitInfo.transform.GetChild(1);
            }
            else
            {
                canMove = false;
                Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.right) * 0.9f, Color.green);
            }

            if (IsSameDir()) 
            {
                canPlay = true;
            }
            else
            {
                canPlay = false;
            }
        }
        else
        {
            canMove = false;
            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.right) * 0.9f, Color.green);
            canPlay = false;
        }
    }

    private void OnTriggerEnter(Collider item)
    {
        if (item.CompareTag("Item"))
        {
            isItemExist = true;
        }
    }

    private void OnTriggerExit(Collider item)
    {
        if (item.CompareTag("Item"))
        {
            isItemExist = false;
        }
    }

    private void OnTriggerStay(Collider item)
    {
        if (item.CompareTag("Item"))
        {
            itemTransform = item.transform;

            if (hitTransform != null && !item.GetComponent<Item>().isMoving && canMove)
            {
                StartCoroutine(CarryItem(itemTransform, hitTransform));
                itemTransform.GetComponent<Item>().EnMove();
            }
        }
    }

    /// <summary>
    /// 물건을 운반하는 함수
    /// </summary>
    public IEnumerator CarryItem(Transform itemTransform, Transform hitTransform)
    {
        float threshold = 0.01f; // 조정 필요한 보정값

        while (Vector3.Distance(itemTransform.position, hitTransform.position) > threshold)
        {
            itemTransform.position = Vector3.MoveTowards(itemTransform.position, hitTransform.position, Time.deltaTime * moveSpeed);
            yield return null;
        }

        // 보정값 적용 후 도착한 지점에 대한 추가 작업 수행
        itemTransform.position = hitTransform.position;
        itemTransform.GetComponent<Item>().UnMove();
    }
}
