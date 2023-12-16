



public class ConveyorBeltBuilding : BasicBuilding
{
    private void OnMouseDown()
    {
        RotateTransform();
        ShowEffect();
    }

    private void Update()
    {
        ChangeSprite();
    }

    private void LateUpdate()
    {
        LookCameraRotation();
    }
}
