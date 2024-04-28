using DG.Tweening;
using UnityEngine;

public class DoubleBasicBuilding : MonoBehaviour
{
    #region Variables
    [SerializeField] bool isStartRotated;
    [SerializeField] bool isRotated;
    public GameObject[] buildings;
    [SerializeField] Transform spriteTransform;
    [SerializeField] Animator animator;
    [SerializeField] bool canExchange;
    [HideInInspector] public Vector3 originScale;
    Sequence buildingScaleSequence;

    [SerializeField] float targetAngle = 180;
    [SerializeField] float startScaleTime = 0.1f; 
    [SerializeField] float endScaleTime = 0.5f;
    [SerializeField] Ease startScaleEase = Ease.OutSine;
    [SerializeField] Ease endScaleEase = Ease.OutElastic;

    BasicBuilding firstBuilding;
    BasicBuilding secondBuilding;
    #endregion
    #region Functions
    private void ExchangeBuildings()
    {
        Vector3 temp = buildings[0].transform.position;
        buildings[0].transform.position = buildings[1].transform.position;
        buildings[1].transform.position = temp;
    }

    private void ShowEffect()
    {
        buildingScaleSequence = DOTween.Sequence().SetAutoKill(true)
        .Append(spriteTransform.DOScale(new Vector3(spriteTransform.localScale.x / 1.5f, spriteTransform.localScale.y / 1.25f, spriteTransform.localScale.z / 1.5f), startScaleTime).SetEase(startScaleEase))
        .Append(spriteTransform.DOScale(originScale, endScaleTime).SetEase(endScaleEase));
    }

    private void Rotate(bool canShowEffect)
    {
        isRotated = !isRotated;

        if (canShowEffect)
            ShowEffect();

        firstBuilding.DirectRotation(false, targetAngle, firstBuilding.transform, true);
        secondBuilding.DirectRotation(false, targetAngle, secondBuilding.transform, true);

        if (canExchange)
            ExchangeBuildings();
    }
    #endregion
    #region Events
    private void Start()
    {
        firstBuilding = buildings[0].GetComponent<BasicBuilding>();
        secondBuilding = buildings[1].GetComponent<BasicBuilding>();

        originScale = spriteTransform.localScale;

        if (!isStartRotated)
        {
            return;
        }

        Rotate(false);
    }

    private void Update()
    {
        if (isRotated)
            animator.SetFloat("Rotated", 1);
        else
            animator.SetFloat("Rotated", -1);
    }

    private void OnMouseOver()
    {
        if ((Input.GetMouseButtonDown(0) || Input.GetMouseButtonDown(1) || Input.GetMouseButtonDown(2)) &&
            firstBuilding.canRotate &&
            secondBuilding.canRotate &&
            !firstBuilding.isRotating &&
            !secondBuilding.isRotating)
        {
            Rotate(true);
        }
    }

    private void OnMouseEnter()
    {
        firstBuilding.direction.SetActive(true);
        secondBuilding.direction.SetActive(true);

        if (firstBuilding.interactionType == interactionType.storeType)
            firstBuilding.itemPanel.SetActive(false);

        if (secondBuilding.interactionType == interactionType.storeType)
            secondBuilding.itemPanel.SetActive(false);
    }

    private void OnMouseExit()
    {
        firstBuilding.direction.SetActive(false);
        secondBuilding.direction.SetActive(false);

        if (firstBuilding.interactionType == interactionType.storeType)
            firstBuilding.itemPanel.SetActive(true);

        if (secondBuilding.interactionType == interactionType.storeType)
            secondBuilding.itemPanel.SetActive(true);
    }
    #endregion
}
