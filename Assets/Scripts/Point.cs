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
                        // 이동형이 직선형일 때, 건물끼리 바라보는 방향이 같은 경우
                        ((hitInfo.transform.GetComponent<ConveyorBeltBuilding>().moveType == moveType.straightType &&
                        (int)hitInfo.transform.eulerAngles.y == (int)transform.parent.eulerAngles.y) ||

                        // 이동형이 곡선형일 때, 바라보는 건물이 해당 건물보다 방향이 90도 돌아가 있는 경우 
                        (hitInfo.transform.GetComponent<ConveyorBeltBuilding>().moveType == moveType.curveType &&
                        (int)hitInfo.transform.eulerAngles.y == (int)(transform.parent.eulerAngles.y) + 90) ||

                        // 이동형이 곡선형일 때, 바라보는 건물의 방향이 0도이고, 해당 건물이 바라보는 건물보다 270도 돌아가 있는 경우
                        hitInfo.transform.GetComponent<ConveyorBeltBuilding>().moveType == moveType.curveType &&
                        (int)hitInfo.transform.eulerAngles.y == 0 &&
                        (int)hitInfo.transform.eulerAngles.y == (int)(transform.parent.eulerAngles.y) - 270);

                    return dir;
                }

                // 바라보는 건물에 아이템이 존재하지 않을 때
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
    /// 물건을 운반하는 함수
    /// </summary>
    public IEnumerator CarryItem(Transform itemTransform, Transform hitTransform)
    {
        float threshold = 0.01f; // 조정 필요한 보정값

        while (itemTransform != null && Vector3.Distance(itemTransform.position, hitTransform.position) > threshold)
        {
            itemTransform.position = Vector3.MoveTowards(itemTransform.position, hitTransform.position, Time.deltaTime * moveSpeed);
            yield return null;
        }

        // 아이템이 삭제되지 않았을 때만 추가 작업 수행
        if (itemTransform != null)
        {
            // 보정값 적용 후 도착한 지점에 대한 추가 작업 수행
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

    /// <summary>
    /// 추출자 신호를 받는 함수
    /// </summary>
    public void ReceiveSendingSignal(WaitForSeconds sendTime)
    {
        isExpectToSend = true;
        StartCoroutine(ReleaseSendingSignal(sendTime));
    }

    /// <summary>
    /// 추출자 신호를 해제하는 함수
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
