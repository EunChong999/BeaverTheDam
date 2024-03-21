using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Balancer : DoubleBasicBuilding
{
    Transform curItem;
    [SerializeField] ConveyorBelt[] Belts;
    public int curCount;
    private void Start()
    {
        Belts = new ConveyorBelt[buildings.Length];

        for(int i = 0; i < Belts.Length; i++)
        {
            Belts[i] = buildings[i].GetComponent<ConveyorBelt>();
        }
    }
    private void Update()
    {
       for(int i = 0; i < Belts.Length; i++)
       {
           var build = Belts[i].point;
           if(build.itemTransform != null 
           && build.itemTransform != curItem 
           && Vector3.Distance(build.itemTransform.position,build.transform.position) <= 0.07f)
           {
               curItem = build.itemTransform;
               int curIndex = CalculateMoveConveyor(i);
               build.itemTransform.position 
               = Belts[curIndex].transform.position;
               curCount++;
           }
       }
    }
    public int CalculateMoveConveyor(int startIndex)
    {
       for(int i = 0; i < Belts.Length; i++)
       {
           if(Belts[(startIndex + i + curCount) % Belts.Length].canMove)
           {
               return (startIndex + i + curCount) % Belts.Length;
           }
       }
       return curCount % Belts.Length;
    }
}
