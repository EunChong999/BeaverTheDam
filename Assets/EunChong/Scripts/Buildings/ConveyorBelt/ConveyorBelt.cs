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
    #region Variables
    [Header("ConveyorBelt")]

    [Space(10)]

    public moveType moveType;
    #endregion

    #region Functions
    /// <summary>
    /// 트레일러로 물건을 운반하는 함수
    /// </summary>
    public void CarryItem()
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

    /// <summary>
    /// 직선으로 운반을 처리하는 함수
    /// </summary>
    public void CarryInStraight()
    {
        transform.Find("Sprite").GetComponent<Animator>().SetBool("IsStraight", true);
    }

    /// <summary>
    /// 곡선으로 운반을 처리하는 함수
    /// </summary>
    public void CarryInCurve()
    {
        transform.Find("Sprite").GetComponent<Animator>().SetBool("IsStraight", false);
    }

    /// <summary>
    /// 트레일러의 방향을 바꾸는 함수
    /// </summary>
    public void ChangeTrailerDirection()
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
    #endregion
}
