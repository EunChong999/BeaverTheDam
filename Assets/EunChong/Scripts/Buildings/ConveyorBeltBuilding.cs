



public class ConveyorBeltBuilding : BasicBuilding
{
    private void OnMouseDown()
    {
        SetTargetRotation();
    }

    private void Update()
    {
        ChangeSprite();
        ResetRotation();
        RotateTransform();
    }

    private void LateUpdate()
    {
        LookCameraRotation();
    }
}
