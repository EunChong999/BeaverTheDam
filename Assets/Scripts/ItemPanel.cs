using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPanel : MonoBehaviour
{
    [SerializeField] bool isDistinct;

    [SerializeField] float amplitude;
    [SerializeField] float frequency;

    [SerializeField] Transform firSpriteTransform;
    [SerializeField] Transform secSpriteTransform;

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
        backspriteRenderer = backSpriteTransform.GetComponent<SpriteRenderer>();

        if (!isDistinct)
        {
            myFirSpriteRenderer.material.color = BuildingManager.instance.itemPanelColor;
            backspriteRenderer.material.color = BuildingManager.instance.itemPanelColor;
        }

        // 자식 오브젝트의 SpriteRenderer를 찾아 배열에 넣습니다.
        CollectSpriteRenderers();
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

        if (firSpriteTransform != null)
            firSpriteTransform.rotation = Quaternion.Euler(30, 45, 0);

        if (secSpriteTransform != null)
            secSpriteTransform.rotation = Quaternion.Euler(30, 45, 0);

        backSpriteTransform.rotation = Quaternion.Euler(30, 45, 0);
    }

    // SpriteRenderer 배열을 선언합니다.
    private SpriteRenderer[] spriteRenderers;

    void CollectSpriteRenderers()
    {
        // 현재 게임 오브젝트와 그 자식 오브젝트들의 SpriteRenderer를 모두 찾습니다.
        spriteRenderers = GetComponentsInChildren<SpriteRenderer>();

        foreach (SpriteRenderer renderer in spriteRenderers)
        {
            BuildingManager.instance.spriteRenderers.Add(renderer);
        }
    }
}
