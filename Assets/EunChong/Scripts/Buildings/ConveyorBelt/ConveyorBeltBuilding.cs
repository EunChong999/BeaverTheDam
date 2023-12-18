using UnityEngine;

public class ConveyorBeltBuilding : ConveyorBelt
{
    #region Events
    private void OnMouseDown()
    {
        DirectRotation();
    }

    private void Start()
    {
        InitSettings();
        SelectTrailerType();
    }

    private void Update()
    {
        ChangeSprite();
        ChangeTrailerDirection();
    }

    private void LateUpdate()
    {
        LookCameraRotation();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Item"))
        {
            CarryItem();
        }
    }
    #endregion
}
