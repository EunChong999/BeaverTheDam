using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPanel : MonoBehaviour
{
    [SerializeField] Transform firSpriteTransform;
    [SerializeField] Transform secSpriteTransform;
    [SerializeField] Transform backSpriteTransform;
    [SerializeField] float amplitude;
    [SerializeField] float frequency;

    SpriteRenderer myFirSpriteRenderer;
    SpriteRenderer mySecSpriteRenderer;
    SpriteRenderer backspriteRenderer;

    private void Start()
    {
        myFirSpriteRenderer = firSpriteTransform.GetComponent<SpriteRenderer>();
        myFirSpriteRenderer.material.color = BuildingManager.instance.itemPanelColor;

        mySecSpriteRenderer = secSpriteTransform.GetComponent<SpriteRenderer>();
        mySecSpriteRenderer.material.color = BuildingManager.instance.itemPanelColor;

        backspriteRenderer = transform.GetChild(0).GetComponent<SpriteRenderer>();
        backspriteRenderer.material.color = BuildingManager.instance.itemPanelColor;
    }

    private void Update()
    {
        if (myFirSpriteRenderer.sprite != null)
            backSpriteTransform.gameObject.SetActive(true);
        else
            backSpriteTransform.gameObject.SetActive(false);

        float x = transform.position.x;
        float y = Mathf.Sin(Time.time * frequency) * amplitude;
        float z = transform.position.z;
        transform.position = new Vector3(x, y + 2.5f, z);
    }

    private void LateUpdate()
    {
        transform.localRotation = firSpriteTransform.localRotation;
    }
}
