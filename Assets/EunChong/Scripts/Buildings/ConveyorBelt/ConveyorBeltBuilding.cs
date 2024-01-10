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
        SelectTrailerType();
    }

    private void Update()
    {
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
