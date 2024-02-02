using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Direction : MonoBehaviour
{
    void Update()
    {
        if (transform.parent.localRotation.eulerAngles.y >= 0 && transform.parent.localRotation.eulerAngles.y < 90)
        {
            transform.localPosition = new Vector3(-0.1f, 1.25f, -0.125f);
        }
        else if (transform.parent.localRotation.eulerAngles.y >= 90 && transform.parent.localRotation.eulerAngles.y < 180)
        {
            transform.localPosition = new Vector3(0.1f, 1.25f, -0.125f);
        }
        else if (transform.parent.localRotation.eulerAngles.y >= 180 && transform.parent.localRotation.eulerAngles.y < 270)
        {
            transform.localPosition = new Vector3(0.1f, 1.25f, 0.125f);
        }
        else if (transform.parent.localRotation.eulerAngles.y >= 270 && transform.parent.localRotation.eulerAngles.y < 360)
        {
            transform.localPosition = new Vector3(-0.1f, 1.25f, 0.125f);
        }
    }
}
