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
        TouchPlacer.FreeGridCell(GridPosition);
        Destroy(gameObject);
    }

    public void RemoveItem()
    {
        TouchPlacer.FreeGridCell(GridPosition);
    }
}