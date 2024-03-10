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
    }

    private void LateUpdate()
    {
        LookCameraRotation();
    }
    #endregion
}
