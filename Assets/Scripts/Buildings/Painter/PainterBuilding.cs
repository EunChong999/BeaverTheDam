using UnityEngine;

public class PainterBuilding : Painter
{
    #region Events
    private void Start()
    {
        InitSettings();
    }

    private void Update()
    {
        
    }

    private void LateUpdate()
    {
        LookCameraRotation();
    }
    #endregion
}
