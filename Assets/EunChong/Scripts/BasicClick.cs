using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicClick : MonoBehaviour
{
    [SerializeField] Material fromMaterial;
    [SerializeField] Material toMaterial;

    [SerializeField] float targetRotation;
    [SerializeField] float rotationSpeed;

    public void MaterialConvert()
    {
        if (gameObject.GetComponent<MeshRenderer>().sharedMaterial == fromMaterial)
        {
            Debug.Log("fromMaterial");
            gameObject.GetComponent<MeshRenderer>().sharedMaterial = toMaterial;
        }
        else
        {
            Debug.Log("toMaterial");
            gameObject.GetComponent<MeshRenderer>().sharedMaterial = fromMaterial;
        }
    }

    public void ObjRotate()
    {
        targetRotation += 90;
    }

    private void Update()
    {
        if (transform.rotation.eulerAngles.y < targetRotation)
        {
            transform.Rotate(0, rotationSpeed * Time.deltaTime, 0);
        }
        else
        {
            transform.eulerAngles = new Vector3(0, targetRotation, 0);
        }
    }
}
