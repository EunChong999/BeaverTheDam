using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Balancer : DoubleBasicBuilding
{
    public int gateCount = 0;
    public bool[] isEnter;
    public Transform enterItem;
    protected override void Start()
    {
        isEnter = new bool[buildings.Length];
    }
    private void Update()
    {
        for (int i = 0; i < buildings.Length; i++)
        {
            Transform curItem = buildings[i].point.itemTransform;
            if (curItem != null && !isEnter[i])
            {
                if (curItem != enterItem)
                {
                    enterItem = curItem;
                    ChangePos();
                }
            }
            isEnter[i] = buildings[i].isItemExist;
        }
    }

    private void ChangePos()
    {
        do
        {
            if(buildings[gateCount % buildings.Length].canMove) 
            {
                break;
            }
            gateCount++;
            gateCount = gateCount % buildings.Length;
        }
        while(!buildings[gateCount % buildings.Length].canMove);
        print(gateCount);
        enterItem.position = buildings[gateCount].transform.position;
        gateCount++;
    }

}
