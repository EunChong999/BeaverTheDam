using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    public bool isMoving;
    public Transform spriteTransform;

    public void EnMove()
    {
        isMoving = true;
    }

    public void UnMove()
    {
        isMoving = false;
    }
}
