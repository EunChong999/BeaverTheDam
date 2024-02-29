using DG.Tweening;
using System.Collections;
using UnityEngine;

public class Point : MonoBehaviour
{
    [SerializeField] LayerMask layerMask;
    [SerializeField] float maxDistance;
    [SerializeField] float startScaleTime;
    [SerializeField] float endScaleTime;
    [SerializeField] Ease startScaleEase;
    [SerializeField] Ease endScaleEase;

    public Vector3 originScale;

    public bool canMove;
    public bool canPlay { get; private set; }
    public bool isItemExist;

    public Transform itemTransform;
    public Transform hitTransform;
    Sequence itemScaleSequence;
    float moveSpeed;

    [HideInInspector] public bool diffDir;

    private void Start()
    {
        if (itemTransform != null)
        {
            isItemExist = true;
        }
    }

    void Update()
    {
        moveSpeed = BuildingManager.instance.speed;

        CanMove();

        if (itemTransform != null
            && hitTransform != null
            && !itemTransform.GetComponent<Item>().isMoving
            && canMove)
        {
            hitTransform.GetComponent<Point>().isItemExist = true;
            StartCoroutine(CarryItem(itemTransform, hitTransform));
            itemTransform.GetComponent<Item>().EnMove();
        }
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

                bool IsDiffDir()
                {
                    bool dir =
                        // �̵����� �������� ��, �ǹ����� �ٶ󺸴� ������ �ݴ��� ���
                        ((hitInfo.transform.GetComponent<ConveyorBeltBuilding>().moveType == moveType.straightType &&
                        Mathf.Abs((int)hitInfo.transform.eulerAngles.y - (int)transform.parent.eulerAngles.y) == 180) ||

                        // �̵����� ����� ��, �ǹ����� �ٶ󺸴� ������ �ݴ��� ���
                        (hitInfo.transform.GetComponent<ConveyorBeltBuilding>().moveType == moveType.curveType &&
                        Mathf.Abs((int)hitInfo.transform.eulerAngles.y - (int)transform.parent.eulerAngles.y) == 180) ||

                        // �̵����� ����� ��, �ٶ󺸴� �ǹ��� ������ 0���̰�, �ǹ����� �ٶ󺸴� ������ �ݴ��� ���
                        hitInfo.transform.GetComponent<ConveyorBeltBuilding>().moveType == moveType.curveType &&
                        (int)hitInfo.transform.eulerAngles.y == 0 &&
                       Mathf.Abs((int)hitInfo.transform.eulerAngles.y - (int)transform.parent.eulerAngles.y) == 180);

                    return dir;
                }

                diffDir = IsDiffDir();
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
                        Mathf.Abs((int)hitInfo.transform.eulerAngles.y - (int)transform.parent.eulerAngles.y) == 90) ||

                        // �̵����� ����� ��, �ٶ󺸴� �ǹ��� ������ 0���̰�, �ش� �ǹ��� �ٶ󺸴� �ǹ����� 270�� ���ư� �ִ� ���
                        hitInfo.transform.GetComponent<ConveyorBeltBuilding>().moveType == moveType.curveType &&
                        (int)hitInfo.transform.eulerAngles.y == 0 &&
                        Mathf.Abs((int)hitInfo.transform.eulerAngles.y - (int)transform.parent.eulerAngles.y) == 270);

                    return dir;
                }

                // �ٶ󺸴� �ǹ��� �������� �������� ���� ��
                if (IsSameDir() && 
                    !hitInfo.transform.GetComponent<ConveyorBeltBuilding>().isItemExist)
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

            canPlay = true;
        }
        else
        {
            canMove = false;
            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.right) * 0.9f, Color.green);
            canPlay = false;
        }
    }

    /// <summary>
    /// ������ ����ϴ� �Լ�
    /// </summary>
    public IEnumerator CarryItem(Transform item, Transform hit)
    {
        float threshold = 0.01f; // ���� �ʿ��� ������

        while (item != null && Vector3.Distance(item.position, hit.position) > threshold)
        {
            item.position = Vector3.MoveTowards(item.position, hit.position, Time.deltaTime * moveSpeed);
            yield return null;
        }

        // �������� �������� �ʾ��� ���� �߰� �۾� ����
        if (item != null)
        {
            // ������ ���� �� ������ ������ ���� �߰� �۾� ����
            item.position = hit.position;
            item.GetComponent<Item>().UnMove();
        }

        Release();
    }

    private void Release()
    {
        hitTransform.GetComponent<Point>().itemTransform = itemTransform;
        itemTransform = null;
        isItemExist = false;
        hitTransform.GetComponent<Point>().isItemExist = true;
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
}
