using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPanel : MonoBehaviour
{
    [SerializeField] float amplitude;
    [SerializeField] float frequency;

    SpriteRenderer myFirSpriteRenderer;
    SpriteRenderer backspriteRenderer;

    [HideInInspector] public Transform spriteTransform;
    [HideInInspector] public Transform backSpriteTransform;

    private void Awake()
    {
        spriteTransform = transform.GetChild(0);
        backSpriteTransform = transform.GetChild(1);
    }

    private void Start()
    {
        myFirSpriteRenderer = spriteTransform.GetComponent<SpriteRenderer>();
        myFirSpriteRenderer.material.color = BuildingManager.instance.itemPanelColor;

        backspriteRenderer = backSpriteTransform.GetComponent<SpriteRenderer>();
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
        spriteTransform.rotation = Quaternion.Euler(30, 45, 0);
        backSpriteTransform.rotation = Quaternion.Euler(30, 45, 0);
    }
}
