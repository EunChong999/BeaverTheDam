using DG.Tweening.Core.Easing;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingManager : MonoBehaviour
{
    [SerializeField] InputControls inputActions;

    public List<SpriteRenderer> spriteRenderers;
    public int buildingCount;
    public float standardHeight;
    public float speed;
    public Vector3 originScale;
    public Color directionColor;
    public Color itemPanelColor;

    public static BuildingManager instance;

    void Awake()
    {
        instance = this; 
    }

    private void Update()
    {

    }
}
