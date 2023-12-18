using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConveyorBelt : BasicBuilding
{
    #region Functions
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
        yield return null;
    }

    /// <summary>
    /// 곡선으로 운반을 처리하는 함수
    /// </summary>
    IEnumerator CarryInCurve()
    {
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
