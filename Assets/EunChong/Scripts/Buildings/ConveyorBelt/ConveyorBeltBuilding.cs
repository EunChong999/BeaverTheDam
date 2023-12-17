
public class ConveyorBeltBuilding : ConveyorBelt
{
    #region Events
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
        ChangeTrailerDirection();
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
