public class CutterBuilding : Cutter
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
        DirectStoreItem();
        //DirectReturnItem();
    }

    private void LateUpdate()
    {
        LookCameraRotation();
    }
    #endregion
}
