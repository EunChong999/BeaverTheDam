using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Balancer : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Item"))
        {
            other.GetComponent<Item>().spriteTransform.GetComponent<SpriteRenderer>().sortingOrder = 2;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Item"))
        {
            other.GetComponent<Item>().spriteTransform.GetComponent<SpriteRenderer>().sortingOrder = 3;
        }
    }
}
