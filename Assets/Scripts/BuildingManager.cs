using DG.Tweening.Core.Easing;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingManager : MonoBehaviour
{
    public float speed;
    public Vector3 originScale;

    public static BuildingManager instance;

    void Awake()
    {
        instance = this; 
    }
}
