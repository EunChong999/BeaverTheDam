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
    /// Ʈ���Ϸ��� ������ ����ϴ� �Լ�
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
    /// �������� ����� ó���ϴ� �Լ�
    /// </summary>
    public void CarryInStraight()
    {
        transform.Find("Sprite").GetComponent<Animator>().SetBool("IsStraight", true);
    }

    /// <summary>
    /// ����� ����� ó���ϴ� �Լ�
    /// </summary>
    public void CarryInCurve()
    {
        transform.Find("Sprite").GetComponent<Animator>().SetBool("IsStraight", false);
    }

    /// <summary>
    /// Ʈ���Ϸ��� ������ �ٲٴ� �Լ�
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
