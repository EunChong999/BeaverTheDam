using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionChecker : MonoBehaviour
{
    [SerializeField] int[] orders;

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Item"))
        {
            other.GetComponent<Item>().spriteTransform.GetComponent<SpriteRenderer>().sortingOrder = orders[0];
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Item"))
        {
            other.GetComponent<Item>().spriteTransform.GetComponent<SpriteRenderer>().sortingOrder = orders[1];
        }
    }
}
