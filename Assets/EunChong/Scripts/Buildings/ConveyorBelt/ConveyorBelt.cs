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
    /// Ʈ���Ϸ��� Ÿ���� �����ϴ� �Լ�
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
    /// Ʈ���Ϸ��� ������ ����ϴ� �Լ�
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
    /// �������� ����� ó���ϴ� �Լ�
    /// </summary>
    IEnumerator CarryInStraight()
    {
        Debug.Log("CarryInStraight");

        yield return null;
    }

    /// <summary>
    /// ����� ����� ó���ϴ� �Լ�
    /// </summary>
    IEnumerator CarryInCurve()
    {
        Debug.Log("CarryInCurve");

        yield return null;
    }

    /// <summary>
    /// Ʈ���Ϸ��� ������ �ٲٴ� �Լ�
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
