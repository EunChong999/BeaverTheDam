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
    }

    private void FixedUpdate()
    {
        CarryItem();
    }

    private void LateUpdate()
    {
        LookCameraRotation();
    }
    #endregion
}
