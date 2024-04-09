using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Detector : MonoBehaviour
{
    [SerializeField] BasicBuilding building;
    [SerializeField] Transform detectionTarget;
    [SerializeField] Transform CurveInputPoint;
    [SerializeField] Transform CurveOutputPoint;
    public bool canMove;

    public void ExchangeDetector()
    {
        Vector3 temp = detectionTarget.position;
        detectionTarget.position = transform.position;
        transform.position = temp;
    }

    private void Start()
    {
        if (building.movementType == movementType.curveType)
        {
            detectionTarget.position = CurveInputPoint.position;
            transform.position = CurveOutputPoint.position;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Detection Target"))
        {
            canMove = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Detection Target"))
        {
            canMove = false;
        }
    }
}
