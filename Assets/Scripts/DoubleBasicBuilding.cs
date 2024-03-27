using DG.Tweening;
using UnityEngine;

public enum doubleType
{
    widthType,
    lengthType
}

public class DoubleBasicBuilding : MonoBehaviour
{
    public doubleType doubleType;
    public GameObject[] buildings;
    [SerializeField] Transform spriteTransform;
    [SerializeField] bool canExchange;
    [HideInInspector] public Vector3 originScale;
    Sequence buildingScaleSequence;

    [SerializeField] float targetAngle;
    [SerializeField] Ease rotationEase;
    [SerializeField] float startScaleTime;
    [SerializeField] float endScaleTime;
    [SerializeField] Ease startScaleEase;
    [SerializeField] Ease endScaleEase;

    private void Start()
    {
        originScale = spriteTransform.localScale;
    }

    private void OnMouseOver()
    {
        if (Input.GetMouseButtonDown(0) && 
            buildings[0].GetComponent<BasicBuilding>().canRotate &&
            buildings[1].GetComponent<BasicBuilding>().canRotate)
        {
            ShowEffect();
            buildings[0].GetComponent<BasicBuilding>().DirectRotation(false, targetAngle, buildings[0].GetComponent<BasicBuilding>().transform);
            buildings[1].GetComponent<BasicBuilding>().DirectRotation(false, targetAngle, buildings[1].GetComponent<BasicBuilding>().transform);

            if (canExchange)
                ExchangeBuildings();
        }

        else if (Input.GetMouseButtonDown(1) &&
            buildings[0].GetComponent<BasicBuilding>().canRotate &&
            buildings[1].GetComponent<BasicBuilding>().canRotate)
        {
            ShowEffect();
            buildings[0].GetComponent<BasicBuilding>().DirectRotation(false, targetAngle, buildings[0].GetComponent<BasicBuilding>().transform);
            buildings[1].GetComponent<BasicBuilding>().DirectRotation(false, targetAngle, buildings[1].GetComponent<BasicBuilding>().transform);

            if (canExchange)
                ExchangeBuildings();
        }

        else if (Input.GetMouseButtonDown(2) &&
            buildings[0].GetComponent<BasicBuilding>().canRotate &&
            buildings[1].GetComponent<BasicBuilding>().canRotate)
        {
            ShowEffect();

            if (doubleType == doubleType.widthType) 
            {
                buildings[0].GetComponent<BasicBuilding>().DirectRotation(false, targetAngle, buildings[0].GetComponent<BasicBuilding>().transform);
                buildings[1].GetComponent<BasicBuilding>().DirectRotation(false, targetAngle, buildings[1].GetComponent<BasicBuilding>().transform);
            }
            else
            {
                buildings[0].GetComponent<BasicBuilding>().ChangeDirectionType();
            }
        }
    }

    private void ExchangeBuildings()
    {
        Vector3 temp = buildings[0].transform.position;
        buildings[0].transform.position = buildings[1].transform.position;
        buildings[1].transform.position = temp;
    }

    protected void ShowEffect()
    {
        buildingScaleSequence = DOTween.Sequence().SetAutoKill(true)
        .Append(spriteTransform.DOScale(new Vector3(spriteTransform.localScale.x / 1.5f, spriteTransform.localScale.y / 1.25f, spriteTransform.localScale.z / 1.5f), startScaleTime).SetEase(startScaleEase))
        .Append(spriteTransform.DOScale(originScale, endScaleTime).SetEase(endScaleEase));
    }
}
