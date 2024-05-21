using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Delivers : MonoBehaviour
{
    public int targetCount;

    [SerializeField] GameObject requireItem;
    [SerializeField] GameObject[] requireItems;
    [SerializeField] SpriteRenderer mySpriteRenderer;
    [SerializeField] DeliverBuilding[] deliverBuildings;
    [SerializeField] TextMeshPro text;

    public void AcceptItem(GameObject item)
    {
        if (targetCount <= 1)
        {
            MainManager.instance.clearScore = 3;
            MainManager.instance.End();
        }

        if (targetCount <= 0)
        {
            return;
        }

        if (item.name.Replace("(Clone)", "") != requireItem.name)
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
