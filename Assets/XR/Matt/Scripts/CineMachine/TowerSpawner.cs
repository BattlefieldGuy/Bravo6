using UnityEngine;

public class TowerSpawner : MonoBehaviour
{
    public int ItemToSpawn;
    public bool IsRightSide;

    void Start()
    {
        TouchPlacer touchPlacer = FindFirstObjectByType<TouchPlacer>();
        if (IsRightSide)
            touchPlacer.SpawnItemR(gameObject.transform.position, ItemToSpawn);
        else
            touchPlacer.SpawnItemL(gameObject.transform.position, ItemToSpawn);
    }
}