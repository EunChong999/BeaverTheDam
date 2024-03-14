using UnityEngine;

public class ConveyorBeltBuilding : ConveyorBelt
{
    #region Events
    private void OnMouseOver()
    {
        // ���콺 ��Ŭ��
        if (Input.GetMouseButtonDown(0))
        {
            DirectRotation(false, targetAngle);
        }

        // ���콺 ��Ŭ��
        else if (Input.GetMouseButtonDown(1))
        {
            DirectRotation(true, targetAngle);
        }

        // ���콺 ��Ŭ��
        else if (Input.GetMouseButtonDown(2))
        {
            ChangeDirectionType();
        }
    }

    private void Start()
    {
        InitSettings();
        SetTrailerType();
    }

    private void Update()
    {
        CheckPoint();
        ChangeSprite();
        ChangeTrailerDirection();
    }

    private void LateUpdate()
    {
        LookCameraRotation();
    }
    #endregion
}
