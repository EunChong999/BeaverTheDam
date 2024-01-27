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
                    // �̵����� �������� ��, �ǹ����� �ٶ󺸴� ������ ���� ���
                    ((hitInfo.transform.GetComponent<ConveyorBeltBuilding>().moveType == moveType.straight &&
                    (int)hitInfo.transform.eulerAngles.y == (int)transform.parent.eulerAngles.y) ||

                    // �̵����� ����� ��, �ٶ󺸴� �ǹ��� �ش� �ǹ����� ������ 90�� ���ư� �ִ� ��� 
                    (hitInfo.transform.GetComponent<ConveyorBeltBuilding>().moveType == moveType.curve &&
                    (int)hitInfo.transform.eulerAngles.y == (int)(transform.parent.eulerAngles.y) + 90) ||

                    // �̵����� ����� ��, �ٶ󺸴� �ǹ��� ������ 0���̰�, �ش� �ǹ��� �ٶ󺸴� �ǹ����� 270�� ���ư� �ִ� ���
                    hitInfo.transform.GetComponent<ConveyorBeltBuilding>().moveType == moveType.curve &&
                    (int)hitInfo.transform.eulerAngles.y == 0 &&
                    (int)hitInfo.transform.eulerAngles.y == (int)(transform.parent.eulerAngles.y) - 270);

                return dir;
            }

            // �ٶ󺸴� �ǹ��� �������� �������� ���� ��
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
    /// ������ ����ϴ� �Լ�
    /// </summary>
    public IEnumerator CarryItem(Transform itemTransform, Transform hitTransform)
    {
        float threshold = 0.01f; // ���� �ʿ��� ������

        while (Vector3.Distance(itemTransform.position, hitTransform.position) > threshold)
        {
            itemTransform.position = Vector3.MoveTowards(itemTransform.position, hitTransform.position, Time.deltaTime * moveSpeed);
            yield return null;
        }

        // ������ ���� �� ������ ������ ���� �߰� �۾� ����
        itemTransform.position = hitTransform.position;
        itemTransform.GetComponent<Item>().UnMove();
    }
}
