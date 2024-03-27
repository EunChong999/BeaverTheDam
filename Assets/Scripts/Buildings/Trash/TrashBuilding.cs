using UnityEngine;

public class TrashBuilding : Trash
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
