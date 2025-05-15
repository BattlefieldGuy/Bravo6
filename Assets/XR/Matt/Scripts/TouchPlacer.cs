using UnityEngine;

public class TouchPlacer : MonoBehaviour
{
    public GridManager grid;
    public GameObject towerPrefab;

    private bool[,] gridOcupied;

    private void Start()
    {
        gridOcupied = new bool[grid.width, grid.height];
    }

    void Update()
    {
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
            HandleTap(Input.GetTouch(0).position);

#if UNITY_EDITOR
        if (Input.GetMouseButtonDown(0))
            HandleTap(Input.mousePosition);
#endif
    }

    void HandleTap(Vector2 screenPos)
    {
        Ray ray = Camera.main.ScreenPointToRay(screenPos);
        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            Vector3 hitPos = hit.point;
            Vector2Int coords = grid.GetGridCoordinates(hitPos);

            if (grid.IsInBounds(coords.x, coords.y))
            {
                if (!gridOcupied[coords.x, coords.y])
                {
                    gridOcupied[coords.x, coords.y] = true;
                    Vector3 spawnPos = grid.GetWorldPosition(coords.x, coords.y) + new Vector3(grid.cellSize, 0, grid.cellSize) * 0.5f;
                    Instantiate(towerPrefab, spawnPos, Quaternion.identity);
                }
                else
                    Debug.Log("Tryed to place on an ocupied gridcell");
            }
        }
    }
}