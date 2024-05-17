using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingManager : MonoBehaviour
{
    public List<SpriteRenderer> spriteRenderers;
    public int buildingCount;
    public float standardHeight;
    public float speed;
    public Vector3 originScale;
    public Color directionColor;
    public Color itemPanelColor;
    public static BuildingManager instance;

    private float duration = 1;

    void Awake()
    {
        instance = this;
    }

    private void Update()
    {
        float alphaChange = (1 / duration) * Time.deltaTime;

        if (Input.GetKey(KeyCode.Space))
        {
            IncreaseAlpha(alphaChange);
        }
        else
        {
            DecreaseAlpha(alphaChange);
        }
    }

    private void IncreaseAlpha(float alphaChange)
    {
        foreach (var renderer in spriteRenderers)
        {
            Color color = renderer.color;
            color.a = Mathf.Clamp(color.a + alphaChange, 0, 1);
            renderer.color = color;

            if (!renderer.gameObject.activeSelf && color.a > 0)
            {
                if (renderer.gameObject.name != "Back")
                    renderer.gameObject.SetActive(true);
            }
        }
    }

    private void DecreaseAlpha(float alphaChange)
    {
        foreach (var renderer in spriteRenderers)
        {
            Color color = renderer.color;
            color.a = Mathf.Clamp(color.a - alphaChange, 0, 1);
            renderer.color = color;

            if (renderer.gameObject.activeSelf && color.a == 0)
            {
                renderer.gameObject.SetActive(false);
            }
        }
    }
}
