using UnityEngine;

public class ConveyorBelt : BasicBuilding
{
    #region Variables

    [Header("ConveyorBeltBuilding")]

    [Space(10)]

    public bool canMove;
    public bool canPlay;
    public bool isItemExist;
    int rotationAngle;
    #endregion
    #region Functions
    /// <summary>
    /// Ʈ���Ϸ��� ������ �ٲٴ� �Լ�
    /// </summary>
    protected void ChangeTrailerDirection()
    {
        if (canPlay)
        {
            rotationAngle = Mathf.RoundToInt(transform.eulerAngles.y);

            if (movementType == movementType.straightType)
            {
                if ((rotationAngle >= 0 && rotationAngle < 90) ||
                    (rotationAngle >= 90 && rotationAngle < 180))
                {
                    spriteAnimator.SetFloat("Speed", 1);
                }

                if ((rotationAngle >= 180 && rotationAngle < 270) ||
                    (rotationAngle >= 270 && rotationAngle < 360))
                {
                    spriteAnimator.SetFloat("Speed", -1);
                }
            }

            if (movementType == movementType.curveType)
            {
                if (directionType == directionType.rightType)
                {
                    if (rotationAngle >= 0 && rotationAngle < 90 ||
                       (rotationAngle >= 90 && rotationAngle < 180 ||
                       (rotationAngle >= 180 && rotationAngle < 270)))
                    {
                        spriteAnimator.SetFloat("Speed", 1);
                    }

                    if (rotationAngle >= 270 && rotationAngle < 360)
                    {
                        spriteAnimator.SetFloat("Speed", -1);
                    }
                }
                else
                {
                    if (rotationAngle >= 0 && rotationAngle < 90 ||
                       (rotationAngle >= 90 && rotationAngle < 180 ||
                       (rotationAngle >= 180 && rotationAngle < 270)))
                    {
                        spriteAnimator.SetFloat("Speed", -1);
                    }

                    if (rotationAngle >= 270 && rotationAngle < 360)
                    {
                        spriteAnimator.SetFloat("Speed", 1);
                    }
                }
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
        switch (movementType)
        {
            case movementType.straightType:
                spriteAnimator.SetBool("IsStraight", true);
                break;
            case movementType.curveType:
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
        spriteAnimator.SetFloat("AngleFloat", Mathf.RoundToInt(transform.eulerAngles.y));
        spriteAnimator.SetInteger("AngleInt", Mathf.RoundToInt(transform.eulerAngles.y));
    }
    #endregion
}
