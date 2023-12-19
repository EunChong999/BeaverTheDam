using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConveyorBelt : BasicBuilding
{
    #region Variables
    public bool isCarrying;
    #endregion

    #region Functions
    /// <summary>
    /// 트레일러로 물건을 운반하는 함수
    /// </summary>
    public IEnumerator CarryItem()
    {


        yield return null;
    }

    /// <summary>
    /// 트레일러의 방향을 바꾸는 함수
    /// </summary>
    public void ChangeTrailerDirection()
    {
        if (canMove)
        {
            if (moveType == moveType.straight)
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

            if (moveType == moveType.curve)
            {
                spriteAnimator.SetFloat("Speed", 1);
            }
        }
        else
        {
            spriteAnimator.SetFloat("Speed", 0);
        }
    }
    #endregion
}
