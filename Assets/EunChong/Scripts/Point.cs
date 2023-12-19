using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Point : MonoBehaviour
{
    [SerializeField] LayerMask layerMask;

    public bool canMove { get; private set; }
    public bool isThereItem;

    void Update()
    {
        CanMove();
    }

    public void CanMove()
    {
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.right), out RaycastHit hitInfo, 0.9f, layerMask))
        {
            if ((hitInfo.transform.GetComponent<ConveyorBeltBuilding>().moveType == moveType.straight && 
                (int)hitInfo.transform.eulerAngles.y == (int)transform.parent.eulerAngles.y) ||

                (hitInfo.transform.GetComponent<ConveyorBeltBuilding>().moveType == moveType.curve &&
                (int)hitInfo.transform.eulerAngles.y == (int)(transform.parent.eulerAngles.y) + 90) ||

                (hitInfo.transform.GetComponent<ConveyorBeltBuilding>().moveType == moveType.curve &&
                (int)hitInfo.transform.eulerAngles.y == 0 &&
                (int)hitInfo.transform.eulerAngles.y == (int)(transform.parent.eulerAngles.y) - 270))
            {
                canMove = true;
                Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.right) * hitInfo.distance, Color.red);
            }
            else
            {
                canMove = false;
                Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.right) * 0.9f, Color.green);
            }
        }
        else
        {
            canMove = false;
            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.right) * 0.9f, Color.green);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Item"))
        {
            Debug.Log("IsThereItem");
            isThereItem = true;
        }
    }
}
