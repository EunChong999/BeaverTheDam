public enum moveType
{
    straightType,
    curveType
}

public class ConveyorBelt : BasicBuilding
{
    #region Variables
    public moveType moveType;
    public bool canMove;
    public bool canPlay;
    public bool isItemExist;
    #endregion
    #region Functions
    /// <summary>
    /// 트레일러의 방향을 바꾸는 함수
    /// </summary>
    protected void ChangeTrailerDirection()
    {
        if (canPlay)
        {
            if (moveType == moveType.straightType)
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

            if (moveType == moveType.curveType)
            {
                spriteAnimator.SetFloat("Speed", 1);
            }
        }
        else
        {
            spriteAnimator.SetFloat("Speed", 0);
        }
    }

    /// <summary>
    /// 트레일러의 타입을 선택하는 함수
    /// </summary>
    protected void SetTrailerType()
    {
        switch (moveType)
        {
            case moveType.straightType:
                spriteAnimator.SetBool("IsStraight", true);
                break;
            case moveType.curveType:
                spriteAnimator.SetBool("IsStraight", false);
                break;
        }
    }

    /// <summary>
    /// 다음 포인트로 이동할 수 있는지 확인하는 함수
    /// </summary>
    protected void CheckCanMove()
    {
        canMove = point.canMove;
        canPlay = point.canPlay;
    }

    /// <summary>
    /// 포인트 위에 아이템이 존재하는지 확인하는 함수
    /// </summary>
    protected void CheckIsItemExist()
    {
        isItemExist = point.isItemExist;
    }

    /// <summary>
    /// 회전각에 따라 스프라이트를 변경하는 함수
    /// </summary>
    protected void ChangeSprite()
    {
        spriteAnimator.SetFloat("Angle", transform.eulerAngles.y);
    }
    #endregion
}
