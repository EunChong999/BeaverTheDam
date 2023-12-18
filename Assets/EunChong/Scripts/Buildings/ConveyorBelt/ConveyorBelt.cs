using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConveyorBelt : BasicBuilding
{
    #region Functions
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
        yield return null;
    }

    /// <summary>
    /// ����� ����� ó���ϴ� �Լ�
    /// </summary>
    IEnumerator CarryInCurve()
    {
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
