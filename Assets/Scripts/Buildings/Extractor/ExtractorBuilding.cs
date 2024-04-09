using UnityEngine;

public class ExtractorBuilding : Extractor
{
    #region Events
    private void OnMouseOver()
    {
        // ���콺 ��Ŭ��
        if (Input.GetMouseButtonDown(0) && !isRotating)
        {
            DirectRotation(false, targetAngle, transform);
        }

        // ���콺 ��Ŭ��
        else if (Input.GetMouseButtonDown(1) && !isRotating)
        {
            DirectRotation(true, targetAngle, transform);
        }

        // ���콺 ��Ŭ��
        else if (Input.GetMouseButtonDown(2) && !isRotating)
        {
            ChangeDirectionType();
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
