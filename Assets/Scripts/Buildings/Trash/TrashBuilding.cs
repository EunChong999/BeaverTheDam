using UnityEngine;

public class TrashBuilding : Trash
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
        RemoveItem();
    }

    private void LateUpdate()
    {
        LookCameraRotation();
    }
    #endregion
}
