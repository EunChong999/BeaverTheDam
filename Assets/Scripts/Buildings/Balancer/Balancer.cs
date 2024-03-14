using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Balancer : DoubleBasicBuilding
{
    Transform curItem;
    int curCount;
    //private void Update()
    //{
    //    for(int i = 0; i < buildings.Length; i++)
    //    {
    //        var build = buildings[i].point;
    //        if(build.itemTransform != null 
    //        && build.itemTransform != curItem 
    //        && Vector3.Distance(build.itemTransform.position,build.transform.position) <= 0.01f)
    //        {
    //            curItem = build.itemTransform;
    //            int curIndex = CalculateMoveConveyor(i);
    //            if(curIndex == -1) return;
    //            build.itemTransform.position 
    //            = buildings[curIndex].transform.position;
    //            curCount++;
    //        }
    //    }
    //}
    //public int CalculateMoveConveyor(int startIndex)
    //{
    //    for(int i = 0; i < buildings.Length; i++)
    //    {
    //        if(buildings[(startIndex + i + curCount) % buildings.Length].canMove)
    //        {
    //            return (startIndex + i + curCount) % buildings.Length;
    //        }
    //    }
    //    return -1;
    //}
}
