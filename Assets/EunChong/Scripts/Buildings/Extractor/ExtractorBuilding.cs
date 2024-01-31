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
        SetArrowDirection();
        SendItem();
    }

    private void LateUpdate()
    {
        LookCameraRotation();
    }
    #endregion
}
