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
            if (// �̵����� �������� ��, �ǹ����� �ٶ󺸴� ������ ���� ���
                (hitInfo.transform.GetComponent<ConveyorBeltBuilding>().moveType == moveType.straight && 
                (int)hitInfo.transform.eulerAngles.y == (int)transform.parent.eulerAngles.y) ||

                // �̵����� ����� ��, �ٶ󺸴� �ǹ��� �ش� �ǹ����� ������ 90�� ���ư� �ִ� ��� 
                (hitInfo.transform.GetComponent<ConveyorBeltBuilding>().moveType == moveType.curve &&
                (int)hitInfo.transform.eulerAngles.y == (int)(transform.parent.eulerAngles.y) + 90) ||

                // �̵����� ����� ��, �ٶ󺸴� �ǹ��� ������ 0���̰�, �ش� �ǹ��� �ٶ󺸴� �ǹ����� 270�� ���ư� �ִ� ���
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
    /// ������ ����ϴ� �Լ�
    /// </summary>
    public IEnumerator CarryItem(Transform itemTransform, Transform hitTransform)
    {
        while (itemTransform.position != hitTransform.position) 
        {
            itemTransform.position = Vector3.MoveTowards(itemTransform.position, hitTransform.position, Time.deltaTime * moveSpeed);

            yield return null;
        }

        Debug.Log("��ǥ ��ġ ����");

        itemTransform.GetComponent<Item>().UnMove();
    }
}
