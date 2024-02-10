using UnityEngine;

public class ConveyorBelt : BasicBuilding
{
    #region Variables

    [Header("ConveyorBeltBuilding")]

    [Space(10)]

    public bool canMove;
    public bool canPlay;
    public bool isItemExist;
    public bool isExpectToSend;
    #endregion
    #region Functions
    /// <summary>
    /// Ʈ���Ϸ��� ������ �ٲٴ� �Լ�
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
    /// Ʈ���Ϸ��� Ÿ���� �����ϴ� �Լ�
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
    /// ����Ʈ�� Ȯ���ϴ� �Լ�
    /// </summary>
    protected void CheckPoint()
    {
        canMove = point.canMove;
        canPlay = point.canPlay;
        isItemExist = point.isItemExist;
    }

    /// <summary>
    /// ȸ������ ���� ��������Ʈ�� �����ϴ� �Լ�
    /// </summary>
    protected void ChangeSprite()
    {
        spriteAnimator.SetFloat("Angle", transform.eulerAngles.y);
    }
    #endregion
}
