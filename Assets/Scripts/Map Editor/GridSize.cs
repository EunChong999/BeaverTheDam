using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridSize
{
    public int Width { get; private set; }
    public int Height { get; private set; }

    public GridSize(int width, int height)
    {
        Width = width;
        Height = height;
    }

    public bool VerifyPlacement(Vector3Int gridPos, Vector2Int objectSize)
    {
        if (gridPos.x + (objectSize.x - 1) >= -11 && 
            gridPos.x + (objectSize.x - 1) <= 9 &&
            gridPos.y + (objectSize.y - 1) >= -21 &&
            gridPos.y + (objectSize.y - 1) <= -1)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}

