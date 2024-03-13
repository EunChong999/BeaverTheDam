using UnityEngine;

public class ExtractorBuilding : Extractor
{
    #region Events
    private void OnMouseOver()
    {
        // ���콺 ��Ŭ��
        if (Input.GetMouseButtonDown(0))
        {
            DirectRotation(directionType.leftType);
        }

        // ���콺 ��Ŭ��
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
