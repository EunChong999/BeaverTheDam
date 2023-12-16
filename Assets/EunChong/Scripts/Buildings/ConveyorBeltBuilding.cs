



public class ConveyorBeltBuilding : BasicBuilding
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
