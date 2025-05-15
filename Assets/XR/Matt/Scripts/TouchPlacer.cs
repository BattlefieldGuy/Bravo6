using UnityEngine;

public class TouchPlacer : MonoBehaviour
{
    public GridManager grid;
    public GameObject gridTowerPrefab;
    public GameObject gridWallPrefab;

    public int ItemToPlace = 1;

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

    void HandleTap(Vector2 _screenPos)
    {
        Ray _ray = Camera.main.ScreenPointToRay(_screenPos);
        if (Physics.Raycast(_ray, out RaycastHit _hit))
        {
            Vector3 _hitPos = _hit.point;
            Vector2Int _coords = grid.GetGridCoordinates(_hitPos);

            if (grid.IsInBounds(_coords.x, _coords.y))
            {
                if (!gridOcupied[_coords.x, _coords.y])
                {
                    Vector3 _spawnPos = grid.GetWorldPosition(_coords.x, _coords.y) + new Vector3(grid.cellSize, 0, grid.cellSize) * 0.5f;
                    GameObject _item = null;
                    switch (ItemToPlace)
                    {
                        case 1:
                            _item = Instantiate(gridTowerPrefab, _spawnPos, new Quaternion(0, 180, 0, 180));
                            break;
                        case 2:
                            _spawnPos.y = 0.06f;
                            _item = Instantiate(gridWallPrefab, _spawnPos, new Quaternion(0, 90, 0, 90));
                            break;
                        default:
                            break;
                    }

                    if (_item != null)
                    {
                        CellManager _manager = _item.GetComponent<CellManager>();
                        _manager.GridPosition = _coords;
                        _manager.TouchPlacer = this;
                        gridOcupied[_coords.x, _coords.y] = true;
                    }
                    else
                        Debug.LogError("Item to spawn is null");

                }
                else
                    Debug.Log("Tryed to place on an ocupied gridcell");
            }
        }
    }

    public void FreeGridCell(Vector2Int _coords)
    {
        gridOcupied[_coords.x, _coords.y] = false;
    }
}