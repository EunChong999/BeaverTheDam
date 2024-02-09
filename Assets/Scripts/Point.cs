using DG.Tweening;
using System.Collections;
using UnityEditor.PackageManager;
using UnityEngine;

public class Point : MonoBehaviour
{
    [SerializeField] LayerMask layerMask;
    [SerializeField] float moveSpeed;
    [SerializeField] float maxDistance;
    [SerializeField] float startScaleTime;
    [SerializeField] float endScaleTime;
    [SerializeField] Ease startScaleEase;
    [SerializeField] Ease endScaleEase;

    public Vector3 originScale;

    public bool canMove { get; private set; }
    public bool canPlay { get; private set; }
    public bool isItemExist { get; private set; }
    public bool isExpectToSend { get; private set; }

    public Transform itemTransform;
    public Transform hitTransform;
    Sequence itemScaleSequence;

    void Update()
    {
        CanMove();
    }

    public void CanMove()
    {
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.right), out RaycastHit hitInfo, maxDistance, layerMask))
        {
            if (transform.parent.GetComponent<BasicBuilding>().buildingType == buildingType.fixedType)
            {
                canMove = true;
                Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.right) * hitInfo.distance, Color.red);
                hitTransform = hitInfo.transform.GetChild(1);
            }
            else if (hitInfo.transform.GetComponent<BasicBuilding>().buildingType == buildingType.movableType)
            {
                bool IsSameDir()
                {
                    bool dir =
                        // �̵����� �������� ��, �ǹ����� �ٶ󺸴� ������ ���� ���
                        ((hitInfo.transform.GetComponent<ConveyorBeltBuilding>().moveType == moveType.straightType &&
                        (int)hitInfo.transform.eulerAngles.y == (int)transform.parent.eulerAngles.y) ||

                        // �̵����� ����� ��, �ٶ󺸴� �ǹ��� �ش� �ǹ����� ������ 90�� ���ư� �ִ� ��� 
                        (hitInfo.transform.GetComponent<ConveyorBeltBuilding>().moveType == moveType.curveType &&
                        (int)hitInfo.transform.eulerAngles.y == (int)(transform.parent.eulerAngles.y) + 90) ||

                        // �̵����� ����� ��, �ٶ󺸴� �ǹ��� ������ 0���̰�, �ش� �ǹ��� �ٶ󺸴� �ǹ����� 270�� ���ư� �ִ� ���
                        hitInfo.transform.GetComponent<ConveyorBeltBuilding>().moveType == moveType.curveType &&
                        (int)hitInfo.transform.eulerAngles.y == 0 &&
                        (int)hitInfo.transform.eulerAngles.y == (int)(transform.parent.eulerAngles.y) - 270);

                    return dir;
                }

                // �ٶ󺸴� �ǹ��� �������� �������� ���� ��
                if (IsSameDir() && !hitInfo.transform.GetComponent<ConveyorBeltBuilding>().isItemExist && !hitInfo.transform.GetComponent<ConveyorBeltBuilding>().isExpectToSend)
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
            else if (hitInfo.transform.GetComponent<BasicBuilding>().buildingType == buildingType.fixedType)
            {
                canPlay = true;
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

            itemTransform = item.transform;
        }
    }

    private void OnTriggerExit(Collider item)
    {
        if (item.CompareTag("Item"))
        {
            isItemExist = false;

            itemTransform = null;
        }
    }

    private void OnTriggerStay(Collider item)
    {
        if (item.CompareTag("Item"))
        {
            if (isItemExist && hitTransform != null && !item.GetComponent<Item>().isMoving && canMove)
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

        while (itemTransform != null && Vector3.Distance(itemTransform.position, hitTransform.position) > threshold)
        {
            itemTransform.position = Vector3.MoveTowards(itemTransform.position, hitTransform.position, Time.deltaTime * moveSpeed);
            yield return null;
        }

        // �������� �������� �ʾ��� ���� �߰� �۾� ����
        if (itemTransform != null)
        {
            // ������ ���� �� ������ ������ ���� �߰� �۾� ����
            itemTransform.position = hitTransform.position;
            itemTransform.GetComponent<Item>().UnMove();
        }
    }

    /// <summary>
    /// ȸ���� Ʈ���� ȿ���� �ִ� �Լ�
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

    /// <summary>
    /// ������ ��ȣ�� �޴� �Լ�
    /// </summary>
    public void ReceiveSendingSignal(WaitForSeconds sendTime)
    {
        isExpectToSend = true;
        StartCoroutine(ReleaseSendingSignal(sendTime));
    }

    /// <summary>
    /// ������ ��ȣ�� �����ϴ� �Լ�
    /// </summary>
    private IEnumerator ReleaseSendingSignal(WaitForSeconds sendTime)
    {
        yield return sendTime;

        if (itemTransform == null)
        {
            isExpectToSend = false;
        }
    }
}
