using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIInitializer : MonoBehaviour
{
    public Vector3 pos;

    private void Start()
    {
        GetComponent<RectTransform>().transform.position = pos;
    }
}
