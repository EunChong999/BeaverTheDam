using UnityEngine;

public class ExtractorBuilding : Extractor
{
    #region Events
    private void OnMouseOver()
    {
        // 마우스 좌클릭
        if (Input.GetMouseButtonDown(0))
        {
            DirectRotation(directionType.leftType);
        }

        // 마우스 우클릭
        if (Input.GetMouseButtonDown(1))
        {
            DirectRotation(directionType.rightType);
        }
    }

    private void Start()
    {
        InitSettings();
    }

    private void Update()
    {
        DirectSending();
    }

    private void LateUpdate()
    {
        LookCameraRotation();
    }
    #endregion
}
