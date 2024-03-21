using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tunnel : ConveyorBeltBuilding
{
    public enum InOutType
    {
        push,
        pull
    }
    [Header("Tunnel")]
    public InOutType gateType;
    protected override void Update()
    {
        base.Update();
        if (point.itemTransform != null
            && gateType == InOutType.push
           && Vector3.Distance(point.itemTransform.position, transform.position) <= 0.07f)
        {
            point.itemTransform.position = point.hitTransform.position;
        }
    }
}
