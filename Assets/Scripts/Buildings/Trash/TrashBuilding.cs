using UnityEngine;

public class TrashBuilding : Trash
{
    #region Events
    private void OnMouseOver()
    {
        // 마우스 좌클릭
        if (Input.GetMouseButtonDown(0))
        {
            DirectRotation(false, targetAngle, transform);
        }

        // 마우스 우클릭
        else if (Input.GetMouseButtonDown(1))
        {
            DirectRotation(true, targetAngle, transform);
        }

        // 마우스 휠클릭
        else if (Input.GetMouseButtonDown(2))
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
        RemoveItem();
    }

    private void LateUpdate()
    {
        LookCameraRotation();
    }
    #endregion
}
