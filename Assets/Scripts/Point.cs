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

    public bool isMovable;
    public bool canMove { get; private set; }
    public bool canPlay { get; private set; }
    public bool isItemExist;

    public Transform itemTransform;
    public Transform hitTransform;
    Sequence itemScaleSequence;
    float moveSpeed;
    int hitAngle;
    int thisAngle;
    movementType hitMovementType;
    directionType hitDirectionType;

    void Update()
    {
        moveSpeed = BuildingManager.instance.speed;

        CanMove();
    }

    public void CanMove()
    {
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.right), out RaycastHit hitInfo, maxDistance, layerMask) && !transform.parent.GetComponent<BasicBuilding>().isRotating)
        {
            hitAngle = Mathf.RoundToInt(hitInfo.transform.eulerAngles.y);
            thisAngle = Mathf.RoundToInt(transform.parent.eulerAngles.y);
            hitMovementType = hitInfo.transform.GetComponent<BasicBuilding>().movementType;
            hitDirectionType = hitInfo.transform.GetComponent<BasicBuilding>().directionType;

            if (hitInfo.transform.GetComponent<BasicBuilding>().buildingType == buildingType.movableType)
            {
                isMovable = true;
            }
            else
            {
                isMovable = false;
            }

            bool IsSameDir()
            {
                if (hitMovementType == movementType.straightType)
                {
                    if (transform.parent.GetComponent<BasicBuilding>().movementType == movementType.straightType)
                    {
                        if (hitAngle == thisAngle)
                        {
                            return true;
                        }
                        else
                        {
                            return false;
                        }
                    }
                    else
                    {
                        if (transform.parent.GetComponent<BasicBuilding>().directionType == directionType.rightType)
                        {
                            if (hitAngle == thisAngle)
                            {
                                return true;
                            }
                            else
                            {
                                return false;
                            }
                        }
                        else
                        {
                            if ((thisAngle == 0 && hitAngle == 90) ||
                                (thisAngle == 270 && hitAngle == 0) ||
                                (thisAngle == 180 && hitAngle == 90) ||
                                (thisAngle == 90 && hitAngle == 180) || 
                                (thisAngle == 180 && hitAngle == 270))
                            {
                                return true;
                            }
                            else
                            {
                                return false;
                            }
                        }
                    }
                }
                else
                {
                    if (hitDirectionType == directionType.rightType)
                    {
                        if ((hitAngle == (thisAngle + 90)) || (hitAngle == 0 && hitAngle == (thisAngle - 270)))
                        {
                            return true;
                        }
                        else
                        {
                            return false;
                        }
                    }
                    else
                    {
                        if (Mathf.Abs(hitAngle - thisAngle) == 180 || (hitAngle == 90 && thisAngle == 90))
                        {
                            return true;
                        }
                        else
                        {
                            return false;
                        }
                    }
                }
            }

            // 바라보는 건물에 아이템이 존재하지 않을 때
            if (IsSameDir() && !hitInfo.transform.GetChild(1).GetComponent<Point>().isItemExist)
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

            canPlay = true;
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
            isItemExist = false;
            itemTransform = null;
        }
    }

    private void OnTriggerStay(Collider obj)
    {
        if (obj.CompareTag("Item") || obj.CompareTag("Dye"))
        {
            isItemExist = true;
            itemTransform = obj.transform;

            if (isItemExist && hitTransform != null && !obj.GetComponent<Item>().isMoving && canMove && isMovable)
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
