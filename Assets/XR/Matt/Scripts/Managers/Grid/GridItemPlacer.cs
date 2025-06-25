using UnityEngine;

public class GridItemPlacer : MonoBehaviour
{
    public int ItemToPlace;
    public bool IsRightSide;

    private TouchPlacer touchPlacer;
    private ShopManager shopManager;



    private void Start()
    {
        touchPlacer = FindFirstObjectByType<TouchPlacer>();
        shopManager = FindFirstObjectByType<ShopManager>();
    }

    public void SpawnItem(Vector3 _coords)
    {
        if (IsRightSide)
            touchPlacer.SpawnItemR(_coords, ItemToPlace);
        else if (!IsRightSide)
            touchPlacer.SpawnItemL(_coords, ItemToPlace);
        LetGoOfItem();
    }

    public void GrabItem()
    {
        shopManager.IsHoldingItem(ItemToPlace, true);
    }

    private void LetGoOfItem()
    {
        shopManager.IsHoldingItem(ItemToPlace, false);
    }
}