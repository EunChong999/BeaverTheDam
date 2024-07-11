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
        // ���콺 ��ġ���� ���̸� ���ϴ�.
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        // MeshCollider���� �浹 ���θ� Ȯ���մϴ�.
        if (meshCollider.Raycast(ray, out hit, Mathf.Infinity))
        {
            Vector3 mousePosition = inputManager.GetSelectedMapPosition();
            Vector3Int gridPosition = grid.WorldToCell(mousePosition);
            cellIndicator.transform.position = grid.CellToWorld(gridPosition);
        }
        else
        {
            // ���콺 Ŀ���� MeshCollider �ۿ� �ִ� ���
            Debug.Log("Mouse is outside the MeshCollider");
        }

    }
}
