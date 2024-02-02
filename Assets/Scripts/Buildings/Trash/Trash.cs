using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trash : BasicBuilding
{
    #region Variables

    [Header("TrashBuilding")]

    [Space(10)]

    bool isSpawned;

    #endregion
    #region Functions
    protected void RemoveItem()
    {
        if (point.canMove && !isSpawned && !point.hitTransform.GetComponent<Point>().isItemExist)
        {

        }
    }
    #endregion
}
