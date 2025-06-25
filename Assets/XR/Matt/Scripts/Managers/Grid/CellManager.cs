using UnityEngine;

public class CellManager : MonoBehaviour
{
    /// <summary>
    /// !!! Important !!! 
    /// this script is on every object that is placed on the grid
    /// </summary>
    [HideInInspector]
    public Vector2Int GridPosition;
    [HideInInspector]
    public TouchPlacer TouchPlacer;

    public void DestroyItem()
    {
        TouchPlacer.FreeGridCellR(GridPosition);
        Destroy(gameObject);
    }

    public void RemoveItemR()
    {
        TouchPlacer.FreeGridCellR(GridPosition);
    }

    public void RemoveItemL()
    {
        TouchPlacer.FreeGridCellR(GridPosition);
    }
}