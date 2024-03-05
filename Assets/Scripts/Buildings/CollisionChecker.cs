using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionChecker : MonoBehaviour
{
    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Item"))
        {
            other.GetComponent<Item>().spriteTransform.GetComponent<SpriteRenderer>().sortingOrder = 4;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Item"))
        {
            other.GetComponent<Item>().spriteTransform.GetComponent<SpriteRenderer>().sortingOrder = 5;
        }
    }
}
