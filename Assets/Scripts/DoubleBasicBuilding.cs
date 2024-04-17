using DG.Tweening;
using UnityEngine;

public enum doubleType
{
    widthType,
    lengthType
}

public class DoubleBasicBuilding : MonoBehaviour
{
    [SerializeField] bool isRotated;
    [SerializeField] bool isReversed;
    public doubleType doubleType;
    public GameObject[] buildings;
    [SerializeField] Transform spriteTransform;
    [SerializeField] Animator animator;
    [SerializeField] bool canExchange;
    [HideInInspector] public Vector3 originScale;
    Sequence buildingScaleSequence;

    [SerializeField] float targetAngle = 180;
    [SerializeField] Ease rotationEase = Ease.Linear;
    [SerializeField] float startScaleTime = 0.1f; 
    [SerializeField] float endScaleTime = 0.5f;
    [SerializeField] Ease startScaleEase = Ease.OutSine;
    [SerializeField] Ease endScaleEase = Ease.OutElastic;

    private void Start()
    {
        originScale = spriteTransform.localScale;

        if (!isReversed)
        {
            return;
        }

        if (canExchange)
            ExchangeBuildings();
    }

    private void Update()
    {
        if (isRotated)
            animator.SetFloat("Rotated", 1);
        else
            animator.SetFloat("Rotated", -1);

        if (isReversed)
            animator.SetFloat("Reversed", 1);
        else
            animator.SetFloat("Reversed", -1);
    }

    private void OnMouseOver()
    {
        if ((Input.GetMouseButtonDown(0) || Input.GetMouseButtonDown(1) || Input.GetMouseButtonDown(2)) && 
            buildings[0].GetComponent<BasicBuilding>().canRotate &&
            buildings[1].GetComponent<BasicBuilding>().canRotate &&
            !buildings[0].GetComponent<BasicBuilding>().isRotating &&
            !buildings[1].GetComponent<BasicBuilding>().isRotating)
        {
            isRotated = !isRotated;

            ShowEffect();
            buildings[0].GetComponent<BasicBuilding>().DirectRotation(false, targetAngle, buildings[0].GetComponent<BasicBuilding>().transform);
            buildings[1].GetComponent<BasicBuilding>().DirectRotation(false, targetAngle, buildings[1].GetComponent<BasicBuilding>().transform);

            if (canExchange)
                ExchangeBuildings();
        }
    }

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
}
