using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPanel : MonoBehaviour
{
    [SerializeField] Transform spriteTransform;
    [SerializeField] float amplitude;
    [SerializeField] float frequency;

    private void Start()
    {
        GetComponent<SpriteRenderer>().color = BuildingManager.instance.itemPanelColor;
        transform.GetChild(0).GetComponent<SpriteRenderer>().color = BuildingManager.instance.itemPanelColor;
    }

    private void Update()
    {
        float x = transform.position.x;
        float y = Mathf.Sin(Time.time * frequency) * amplitude;
        float z = transform.position.z;
        transform.position = new Vector3(x, y + 2.5f, z);
    }

    private void LateUpdate()
    {
        transform.localRotation = spriteTransform.localRotation;
    }
}
