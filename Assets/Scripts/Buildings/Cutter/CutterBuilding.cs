using UnityEngine;

public class CutterBuilding : Cutter
{
    #region Events
    private void Start()
    {
        InitSettings();
    }

    private void Update()
    {
        DirectStoreItem();
        DirectReturnItem();
    }

    private void LateUpdate()
    {
        LookCameraRotation();
    }
    #endregion
}
