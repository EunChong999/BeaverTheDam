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
    }

    private void LateUpdate()
    {
        LookCameraRotation();
    }
    #endregion
}
