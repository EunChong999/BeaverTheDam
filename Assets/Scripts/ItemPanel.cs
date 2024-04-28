using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPanel : MonoBehaviour
{
    [SerializeField] Transform spriteTransform;
    [SerializeField] float amplitude;
    [SerializeField] float frequency;

    SpriteRenderer mySpriteRenderer;
    SpriteRenderer backspriteRenderer;

    private void Start()
    {
        mySpriteRenderer = GetComponent<SpriteRenderer>();
        mySpriteRenderer.material.color = BuildingManager.instance.itemPanelColor;

        backspriteRenderer = transform.GetChild(0).GetComponent<SpriteRenderer>();
        backspriteRenderer.material.color = BuildingManager.instance.itemPanelColor;
    }

    private void Update()
    {
        if (mySpriteRenderer.sprite != null)
            transform.GetChild(0).gameObject.SetActive(true);
        else
            transform.GetChild(0).gameObject.SetActive(false);

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
