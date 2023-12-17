using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum moveType
{
    straight,
    curve
}

public class ConveyorBelt : BasicBuilding
{
    [Header("ConveyorBelt")]

    [Space(10)]

    public moveType moveType;

    public void CarryTrailer()
    {
        switch (moveType)
        {
            case moveType.straight:
                CarryInStraight();
                break;
            case moveType.curve:
                CarryInCurve();
                break;
        }
    }

    public void CarryInStraight()
    {

    }

    public void CarryInCurve()
    {

    }

    public void ChangeAnimSpeed()
    {
        if (((int)transform.eulerAngles.y >= 0 && (int)transform.eulerAngles.y < 90) ||
            ((int)transform.eulerAngles.y >= 90 && (int)transform.eulerAngles.y < 180))
        {
            transform.Find("Sprite").GetComponent<Animator>().SetFloat("Speed", 1);
        }

        if (((int)transform.eulerAngles.y >= 180 && (int)transform.eulerAngles.y < 270) ||
            ((int)transform.eulerAngles.y >= 270 && (int)transform.eulerAngles.y < 360))
        {
            transform.Find("Sprite").GetComponent<Animator>().SetFloat("Speed", -1);
        }
    }
}
