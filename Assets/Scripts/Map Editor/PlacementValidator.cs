using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlacementValidator
{
    private GridSize gridSize;

    public PlacementValidator(GridSize gridSize)
    {
        this.gridSize = gridSize;
    }

    public bool ValidatePlacement(Vector3Int gridPos, Vector2Int objectSize)
    {
        return gridSize.VerifyPlacement(gridPos, objectSize);
    }
}

