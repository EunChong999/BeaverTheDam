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
        DirectStoreItem();

        if (painterType == painterType.outputType)
            DirectReturnItem();
    }

    private void LateUpdate()
    {
        LookCameraRotation();
    }
    #endregion
}
