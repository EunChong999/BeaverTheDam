using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Point : MonoBehaviour
{
    [SerializeField] LayerMask layerMask;
    [SerializeField] float moveSpeed;

    public bool canMove { get; private set; }

    Transform itemTransform;
    Transform hitTransform;

    void Update()
    {
        CanMove();
    }

    public void CanMove()
    {
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.right), out RaycastHit hitInfo, 0.9f, layerMask))
        {
            if (// 이동형이 직선형일 때, 건물끼리 바라보는 방향이 같은 경우
                (hitInfo.transform.GetComponent<ConveyorBeltBuilding>().moveType == moveType.straight && 
                (int)hitInfo.transform.eulerAngles.y == (int)transform.parent.eulerAngles.y) ||

                // 이동형이 곡선형일 때, 바라보는 건물이 해당 건물보다 방향이 90도 돌아가 있는 경우 
                (hitInfo.transform.GetComponent<ConveyorBeltBuilding>().moveType == moveType.curve &&
                (int)hitInfo.transform.eulerAngles.y == (int)(transform.parent.eulerAngles.y) + 90) ||

                // 이동형이 곡선형일 때, 바라보는 건물의 방향이 0도이고, 해당 건물이 바라보는 건물보다 270도 돌아가 있는 경우
                (hitInfo.transform.GetComponent<ConveyorBeltBuilding>().moveType == moveType.curve &&
                (int)hitInfo.transform.eulerAngles.y == 0 &&
                (int)hitInfo.transform.eulerAngles.y == (int)(transform.parent.eulerAngles.y) - 270))
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
        }
        else
        {
            canMove = false;
            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.right) * 0.9f, Color.green);
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
        while (itemTransform.position != hitTransform.position) 
        {
            itemTransform.position = Vector3.MoveTowards(itemTransform.position, hitTransform.position, Time.deltaTime * moveSpeed);

            yield return null;
        }

        Debug.Log("목표 위치 도착");

        itemTransform.GetComponent<Item>().UnMove();
    }
}
