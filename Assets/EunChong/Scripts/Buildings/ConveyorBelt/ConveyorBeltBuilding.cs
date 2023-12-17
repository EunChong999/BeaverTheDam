



public class ConveyorBeltBuilding : ConveyorBelt
{
    private void OnMouseDown()
    {
        DirectRotation();
    }

    private void Start()
    {
        InitTransformScale();
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
