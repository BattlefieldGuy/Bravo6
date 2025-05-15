using UnityEngine;

public class CellManager : MonoBehaviour
{
    /// <summary>
    /// !!! Important !!! 
    /// this script is on every object that is placed on the grid
    /// </summary>

    public Vector2Int GridPosition;
    public TouchPlacer TouchPlacer;

    public void DestroyItem() => TouchPlacer.FreeGridCell(GridPosition);
}