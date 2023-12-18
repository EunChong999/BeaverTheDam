using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Point : MonoBehaviour
{
    public bool canMove { get; private set; }

    void Update()
    {
        CanMove();
    }

    public void CanMove()
    {
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.right), out RaycastHit hitInfo, 0.9f))
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
}
