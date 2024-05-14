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

        // �ڽ� ������Ʈ�� SpriteRenderer�� ã�� �迭�� �ֽ��ϴ�.
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

    // SpriteRenderer �迭�� �����մϴ�.
    private SpriteRenderer[] spriteRenderers;

    void CollectSpriteRenderers()
    {
        // ���� ���� ������Ʈ�� �� �ڽ� ������Ʈ���� SpriteRenderer�� ��� ã���ϴ�.
        spriteRenderers = GetComponentsInChildren<SpriteRenderer>();

        foreach (SpriteRenderer renderer in spriteRenderers)
        {
            BuildingManager.instance.spriteRenderers.Add(renderer);
        }
    }
}
