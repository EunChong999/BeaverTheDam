using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Detector : MonoBehaviour
{
    [SerializeField] BasicBuilding building;
    [SerializeField] Transform InputDetector;
    [SerializeField] Transform OutputDetector;
    [SerializeField] Transform CurveInputPoint;
    [SerializeField] Transform CurveOutputPoint;
    public bool canMove;

    public void ExchangeDetector()
    {
        Vector3 temp = InputDetector.position;
        InputDetector.position = OutputDetector.position;
        OutputDetector.position = temp;
    }

    private void Start()
    {
        if (building.movementType == movementType.curveType)
        {
            InputDetector.position = CurveInputPoint.position;
            OutputDetector.position = CurveOutputPoint.position;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Detector"))
        {
            canMove = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Detector"))
        {
            canMove = false;
        }
    }
}
