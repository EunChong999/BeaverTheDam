using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlacementSystem : MonoBehaviour
{
    [SerializeField]
    private GameObject cellIndicator;
    [SerializeField]
    private InputManager inputManager;
    [SerializeField]
    private Grid grid;
    [SerializeField]
    private BoxCollider meshCollider;

    private void Update()
    {
        // 마우스 위치에서 레이를 쏩니다.
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        // MeshCollider와의 충돌 여부를 확인합니다.
        if (meshCollider.Raycast(ray, out hit, Mathf.Infinity))
        {
            Vector3 mousePosition = inputManager.GetSelectedMapPosition();
            Vector3Int gridPosition = grid.WorldToCell(mousePosition);
            cellIndicator.transform.position = grid.CellToWorld(gridPosition);
        }
        else
        {
            // 마우스 커서가 MeshCollider 밖에 있는 경우
            Debug.Log("Mouse is outside the MeshCollider");
        }

    }
}
