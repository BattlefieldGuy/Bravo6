using UnityEngine;

public class TouchPlacer : MonoBehaviour
{

    [Header("GridR")]
    public GameObject gridTowerPrefabR;
    public GameObject gridWallPrefabR;

    public GridManager gridR;

    public int ItemToPlaceR = 1;

    private bool[,] gridOcupiedR;

    [Header("GridL")]
    public GameObject gridTowerPrefabL;
    public GameObject gridWallPrefabL;

    public GridManager gridL;

    public int ItemToPlaceL = 1;

    private bool[,] gridOcupiedL;


    private int prizeToReturn;

    #region --- GENERAL ---

    private void Start()
    {
        gridOcupiedR = new bool[gridR.width, gridR.height];
    }

    public int ReturnPrize(int _itemToPlace)
    {
        switch (_itemToPlace)
        {
            case 1:
                prizeToReturn = gridTowerPrefabR.GetComponent<Tower>().Prize;
                Debug.Log(prizeToReturn);
                return prizeToReturn;
            case 2:
                prizeToReturn = gridWallPrefabR.GetComponent<GridWall>().Prize;
                Debug.Log(prizeToReturn);
                return prizeToReturn;
            default:
                return 0;
        }
        ;

    }

    #endregion

    #region --- RIGHT SIDE ---
    public void SpawnItemR(Vector3 _itemPos, int _itemToPlace)
    {
        ItemToPlaceR = _itemToPlace;
        if (Physics.Raycast(_itemPos, Vector3.down, out RaycastHit _hit))
        {
            Vector3 _hitPos = _hit.point;
            Vector2Int _coords = gridR.GetGridCoordinates(_hitPos);

            if (gridR.IsInBounds(_coords.x, _coords.y))
            {
                if (!gridOcupiedR[_coords.x, _coords.y])
                {
                    Vector3 _spawnPos = gridR.GetWorldPosition(_coords.x, _coords.y) + new Vector3(gridR.cellSize, 0, gridR.cellSize) * 0.5f;
                    GameObject _item = null;
                    switch (ItemToPlaceR)
                    {
                        case 1:
                            _item = Instantiate(gridTowerPrefabR, _spawnPos, new Quaternion(0, -1, 0, 0));
                            break;
                        case 2:
                            _spawnPos.y = 0.06f;
                            _item = Instantiate(gridWallPrefabR, _spawnPos, new Quaternion(0, 90, 0, 90));
                            break;
                        default:
                            break;
                    }

                    if (_item != null)
                    {
                        CellManager _manager = _item.GetComponent<CellManager>();
                        _manager.GridPosition = _coords;
                        _manager.TouchPlacer = this;
                        gridOcupiedR[_coords.x, _coords.y] = true;
                    }
                    else
                        Debug.LogError("Item to spawn is null");
                }
                else
                    CoinManager.AddDECoins(prizeToReturn);
            }
            else
                CoinManager.AddDECoins(prizeToReturn);

        }
    }


    public void FreeGridCellR(Vector2Int _coords)
    {
        gridOcupiedR[_coords.x, _coords.y] = false;
    }

    #endregion

    #region --- LEFT SIDE ---

    public void SpawnItemL(Vector3 _itemPos, int _itemToPlace)
    {
        ItemToPlaceL = _itemToPlace;
        if (Physics.Raycast(_itemPos, Vector3.down, out RaycastHit _hit))
        {
            Vector3 _hitPos = _hit.point;
            Vector2Int _coords = gridL.GetGridCoordinates(_hitPos);

            if (gridL.IsInBounds(_coords.x, _coords.y))
            {
                if (!gridOcupiedL[_coords.x, _coords.y])
                {
                    Vector3 _spawnPos = gridL.GetWorldPosition(_coords.x, _coords.y) + new Vector3(gridL.cellSize, 0, gridL.cellSize) * 0.5f;
                    GameObject _item = null;
                    switch (ItemToPlaceL)
                    {
                        case 1:
                            _item = Instantiate(gridTowerPrefabL, _spawnPos, new Quaternion(0, -1, 0, 0));
                            break;
                        case 2:
                            _spawnPos.y = 0.06f;
                            _item = Instantiate(gridWallPrefabL, _spawnPos, new Quaternion(0, 90, 0, 90));
                            break;
                        default:
                            break;
                    }

                    if (_item != null)
                    {
                        CellManager _manager = _item.GetComponent<CellManager>();
                        _manager.GridPosition = _coords;
                        _manager.TouchPlacer = this;
                        gridOcupiedL[_coords.x, _coords.y] = true;
                    }
                    else
                        Debug.LogError("Item to spawn is null");
                }
                else
                    CoinManager.AddATCoins(prizeToReturn);
            }
            else
                CoinManager.AddATCoins(prizeToReturn);

        }
    }

    public void FreeGridCellL(Vector2Int _coords)
    {
        gridOcupiedL[_coords.x, _coords.y] = false;
    }

    #endregion
}