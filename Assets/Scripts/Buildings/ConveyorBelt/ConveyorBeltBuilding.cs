using UnityEngine;

public class ConveyorBeltBuilding : ConveyorBelt
{
    #region Events
    private void OnMouseOver()
    {
        // ���콺 ��Ŭ��
        if (Input.GetMouseButtonDown(0))
        {
            DirectRotation(false, targetAngle, transform);
        }

        // ���콺 ��Ŭ��
        else if (Input.GetMouseButtonDown(1))
        {
            DirectRotation(true, targetAngle, transform);
        }

        // ���콺 ��Ŭ��
        else if (Input.GetMouseButtonDown(2))
        {
            ChangeDirectionType();

            if (movementType == movementType.curveType)
                detector.ExchangeDetector();
        }
    }

    private void Start()
    {
        InitSettings();
        SetTrailerType();
    }

    protected virtual void Update()
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
