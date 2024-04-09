using UnityEngine;

public class ConveyorBeltBuilding : ConveyorBelt
{
    #region Events
    private void OnMouseOver()
    {
        // 마우스 좌클릭
        if (Input.GetMouseButtonDown(0) && !isRotating)
        {
            DirectRotation(false, targetAngle, transform);
        }

        // 마우스 우클릭
        else if (Input.GetMouseButtonDown(1) && !isRotating)
        {
            DirectRotation(true, targetAngle, transform);
        }

        // 마우스 휠클릭
        else if (Input.GetMouseButtonDown(2) && !isRotating)
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
