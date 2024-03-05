using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BalancerBuilding : Balancer
{
    [SerializeField] GameObject[] buildings;

    private void OnMouseDown()
    {
        buildings[0].GetComponent<BasicBuilding>().DirectRotation();
        buildings[1].GetComponent<BasicBuilding>().DirectRotation();
    }
}
