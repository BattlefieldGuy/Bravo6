using UnityEngine;

public class GridManager : MonoBehaviour
{
    [Header("Grid Settings")]
    public int width = 10;
    public int height = 10;
    public float cellSize = 0.2f;
    public Vector3 origin = Vector3.zero;

    [Header("Visuals")]
    public GameObject gridTilePrefab;
    public Transform gridParent;

    private void Start()
    {
        GenerateVisualGrid();
        origin = gridParent.transform.position;
    }

    public void GenerateVisualGrid()
    {
        if (!gridTilePrefab) return;

        for (int x = 0; x < width; x++)
        {
            for (int z = 0; z < height; z++)
            {
                origin = gridParent.transform.position;
                Vector3 pos = origin + new Vector3(x * cellSize, 0, z * cellSize) + new Vector3(cellSize, 0, cellSize) * 0.5f;
                Instantiate(gridTilePrefab, pos, Quaternion.identity, gridParent);
            }
        }
    }


    public Vector3 GetWorldPosition(int x, int z)
    {
        return origin + new Vector3(x * cellSize, 0, z * cellSize);
    }

    public Vector2Int GetGridCoordinates(Vector3 worldPos)
    {
        Vector3 local = worldPos - origin;
        int x = Mathf.FloorToInt(local.x / cellSize);
        int z = Mathf.FloorToInt(local.z / cellSize);
        return new Vector2Int(x, z);
    }

    public bool IsInBounds(int x, int z)
    {
        return x >= 0 && z >= 0 && x < width && z < height;
    }
}
