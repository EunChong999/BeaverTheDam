using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrashBuilding : Trash
{
    #region Events
    private void OnMouseDown()
    {
        DirectRotation();
    }

    private void Start()
    {
        InitSettings();
    }

    private void Update()
    {
        SetArrowDirection();
    }

    private void LateUpdate()
    {
        LookCameraRotation();
    }
    #endregion
}
