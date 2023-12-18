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
    /// 트레일러의 타입을 선택하는 함수
    /// </summary>
    public void SelectTrailerType()
    {
        switch (moveType)
        {
            case moveType.straight:
                spriteAnimator.SetBool("IsStraight", true);
                break;
            case moveType.curve:
                spriteAnimator.SetBool("IsStraight", false);
                break;
        }
    }

    /// <summary>
    /// 트레일러로 물건을 운반하는 함수
    /// </summary>
    public void CarryItem()
    {
        switch (moveType)
        {
            case moveType.straight:
                StartCoroutine(CarryInStraight());
                break;
            case moveType.curve:
                StartCoroutine(CarryInCurve());
                break;
        }
    }

    /// <summary>
    /// 직선으로 운반을 처리하는 함수
    /// </summary>
    IEnumerator CarryInStraight()
    {
        Debug.Log("CarryInStraight");

        yield return null;
    }

    /// <summary>
    /// 곡선으로 운반을 처리하는 함수
    /// </summary>
    IEnumerator CarryInCurve()
    {
        Debug.Log("CarryInCurve");

        yield return null;
    }

    /// <summary>
    /// 트레일러의 방향을 바꾸는 함수
    /// </summary>
    public void ChangeTrailerDirection()
    {
        if (((int)transform.eulerAngles.y >= 0 && (int)transform.eulerAngles.y < 90) ||
            ((int)transform.eulerAngles.y >= 90 && (int)transform.eulerAngles.y < 180))
        {
            spriteAnimator.SetFloat("Speed", 1);
        }

        if (((int)transform.eulerAngles.y >= 180 && (int)transform.eulerAngles.y < 270) ||
            ((int)transform.eulerAngles.y >= 270 && (int)transform.eulerAngles.y < 360))
        {
            spriteAnimator.SetFloat("Speed", -1);
        }
    }
    #endregion
}
