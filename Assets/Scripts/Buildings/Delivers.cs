using TMPro;
using UnityEngine;

public class Delivers : MonoBehaviour
{
    public int targetCount;
    public bool isAllAccepted;

    [SerializeField] DeliversManager manager;
    [SerializeField] TargetItemChecker checker;
    [SerializeField] GameObject requireItem;
    [SerializeField] GameObject[] requireItems;
    [SerializeField] SpriteRenderer mySpriteRenderer;
    [SerializeField] DeliverBuilding[] deliverBuildings;
    [SerializeField] TextMeshPro text;

    public void AcceptItem(GameObject item)
    {
        if (isAllAccepted)
            return;

        if (checker.isCutted && checker.isPainted)
        {
            if (item.GetComponent<Item>().myColorType != checker.targetColor || !item.GetComponent<Item>().isCutted)
            {
                return;
            }
        }
        else
        {
            if (checker.isMixed)
                if (item.GetComponent<Dye>().myColorType != checker.targetColor)
                    return;

            if (checker.isPainted)
                if (item.GetComponent<Item>().myColorType != checker.targetColor)
                    return;

            if (checker.isCutted)
                if (!item.GetComponent<Item>().isCutted)
                    return;
        }

        targetCount--;

        if (targetCount <= 0)
        {
            isAllAccepted = true;

            if (manager != null)
                return;

            if (MainManager.instance.curStage.type == MapType.time)
            {
                if (MainManager.instance.integratedCount >= MainManager.instance.curStage.firstTimeLimit)
                    MainManager.instance.clearScore = 3;
                else if (MainManager.instance.integratedCount >= MainManager.instance.curStage.secondTimeLimit)
                    MainManager.instance.clearScore = 2;
                else
                    MainManager.instance.clearScore = 1;
            }
            else if (MainManager.instance.curStage.type == MapType.count)
            {
                if (MainManager.instance.integratedCount >= MainManager.instance.curStage.firstCountLimit)
                    MainManager.instance.clearScore = 3;
                else if (MainManager.instance.integratedCount >= MainManager.instance.curStage.secondCountLimit)
                    MainManager.instance.clearScore = 2;
                else
                    MainManager.instance.clearScore = 1;
            }

            MainManager.instance.End(true);
        }
    }
    
    private void Start()
    {
        mySpriteRenderer.sprite = requireItem.transform.GetComponent<Item>().spriteRenderer.sprite;
    }

    private void Update()
    {
        text.text = targetCount.ToString();
    }
}
