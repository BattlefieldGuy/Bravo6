using UnityEngine;

public class GridItemPlacer : MonoBehaviour
{
    [SerializeField] private int itemToPlace;
    private TouchPlacer touchPlacer;

    private void Start()
    {
        touchPlacer = FindFirstObjectByType<TouchPlacer>();
    }

    public void SpawnItem(Vector2 _coords)
    {
        touchPlacer.SpawnItem(_coords, itemToPlace);
    }
}