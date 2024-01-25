using UnityEngine;

public class ConveyorBeltBuilding : ConveyorBelt
{
    #region Events
    private void OnMouseDown()
    {
        DirectRotation();
    }

    private void Start()
    {
        InitSettings();
        SetTrailerType();
    }

    private void Update()
    {
        SetArrowDirection();
        ChangeSprite();
        ChangeTrailerDirection();
        CheckCanMove();
        CheckIsItemExist();
    }

    private void LateUpdate()
    {
        LookCameraRotation();
    }
    #endregion
}
