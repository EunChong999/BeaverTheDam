using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridManager : MonoBehaviour
{
    private GridSize gridSize;
    private PlacementValidator placementValidator;
    public static GridManager instance;

    private void Awake()
    {
        instance = this;
    }

    void Start()
    {
        gridSize = new(); 
        placementValidator = new PlacementValidator(gridSize);
    }

    public bool TryPlaceObject(Vector3Int gridPos, Vector2Int objectSize)
    {
        if (placementValidator.ValidatePlacement(gridPos, objectSize))
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}

