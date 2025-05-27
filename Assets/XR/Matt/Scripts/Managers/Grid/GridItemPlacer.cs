using UnityEngine;

public class GridItemPlacer : MonoBehaviour
{
    public int ItemToPlace;
    private TouchPlacer touchPlacer;

    private void Start()
    {
        touchPlacer = FindFirstObjectByType<TouchPlacer>();
    }

    public void SpawnItem(Vector3 _coords)
    {
        touchPlacer.SpawnItem(_coords, ItemToPlace);
    }
}