public class ExtractorBuilding : Extractor
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
        SendItem();
    }

    private void LateUpdate()
    {
        LookCameraRotation();
    }
    #endregion
}
