using TMPro;
using UnityEngine;

public class Delivers : MonoBehaviour
{
    public int targetCount;

    [SerializeField] TargetItemChecker checker;
    [SerializeField] GameObject requireItem;
    [SerializeField] GameObject[] requireItems;
    [SerializeField] SpriteRenderer mySpriteRenderer;
    [SerializeField] DeliverBuilding[] deliverBuildings;
    [SerializeField] TextMeshPro text;

    public void AcceptItem(GameObject item)
    {
        if (targetCount <= 1)
        {
            if (MainManager.instance.curStage.type == MapType.timeType)
            {
                if (MainManager.instance.integratedCount >= MainManager.instance.curStage.limitTime1)
                    MainManager.instance.clearScore = 3;
                else if (MainManager.instance.integratedCount >= MainManager.instance.curStage.limitTime2)
                    MainManager.instance.clearScore = 2;
                else 
                    MainManager.instance.clearScore = 1;
            }
            else if (MainManager.instance.curStage.type == MapType.countType)
            {
                if (MainManager.instance.integratedCount >= MainManager.instance.curStage.count1)
                    MainManager.instance.clearScore = 3;
                else if (MainManager.instance.integratedCount >= MainManager.instance.curStage.count2)
                    MainManager.instance.clearScore = 2;
                else 
                    MainManager.instance.clearScore = 1;
            }

            MainManager.instance.End(true);
        }

        if (targetCount <= 0)
        {
            return;
        }

        if (item.name.Replace("(Clone)", "") != requireItem.name || item.GetComponent<Item>().firstColor != requireItem.GetComponent<Item>().firstColor)
            return;

        if (checker.isCutted && checker.isPainted)
            if (item.GetComponent<Item>().spriteRenderer.color.ToString() != checker.targetColor.ToString() || !item.GetComponent<Item>().isCutted)
                return;

        if (checker.isPainted)
            if (item.GetComponent<Item>().spriteRenderer.color.ToString() != checker.targetColor.ToString())
                return;

        if (checker.isCutted)
            if (!item.GetComponent<Item>().isCutted)
                return;

        targetCount--;
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
