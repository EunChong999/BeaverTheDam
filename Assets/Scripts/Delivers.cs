using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Delivers : MonoBehaviour
{
    public int targetCount;

    [SerializeField] GameObject requireItem;
    [SerializeField] SpriteRenderer mySpriteRenderer;
    [SerializeField] DeliverBuilding[] deliverBuildings;
    [SerializeField] TextMeshPro text;

    public void AcceptItem(string name)
    {
        if (name.Replace("(Clone)", "") != requireItem.name) 
            return;

        if (targetCount <= 0)
            return;

        targetCount--;

        Debug.Log("È®ÀÎ");
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
