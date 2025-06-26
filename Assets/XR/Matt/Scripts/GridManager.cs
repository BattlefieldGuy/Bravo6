using UnityEngine;

public class GridManager : MonoBehaviour
{
    [Header("Grid Settings")]
    public int width = 10;
    public int height = 10;
    public float cellSize = 0.2f;

    [Header("Right side")]
    public Vector3 originR = Vector3.zero;
    public Transform gridParentR;

    [Header("Left side")]
    public Vector3 originL = Vector3.zero;
    public Transform gridParentL;

    [Header("Visuals")]
    public GameObject gridTilePrefab;

    private void Start()
    {
        GenerateVisualGridR();
        GenerateVisualGridL();
        originR = gridParentR.transform.position;
    }

    public void GenerateVisualGridR()
    {
        if (!gridTilePrefab) return;

        for (int x = 0; x < width; x++)
        {
            for (int z = 0; z < height; z++)
            {
                originR = gridParentR.transform.position;
                Vector3 pos = originR + new Vector3(x * cellSize, 0, z * cellSize) + new Vector3(cellSize, 0, cellSize) * 0.5f;
                Instantiate(gridTilePrefab, pos, Quaternion.identity, gridParentR);
            }
        }
    }

    public void GenerateVisualGridL()
    {
        if (!gridTilePrefab) return;

        for (int x = 0; x < width; x++)
        {
            for (int z = 0; z < height; z++)
            {
                originL = gridParentL.transform.position;
                Vector3 pos = originL + new Vector3(x * cellSize, 0, z * cellSize) + new Vector3(cellSize, 0, cellSize) * 0.5f;
                Instantiate(gridTilePrefab, pos, Quaternion.identity, gridParentL);
            }
        }
    }


    public Vector3 GetWorldPosition(int x, int z)
    {
        return originR + new Vector3(x * cellSize, 0, z * cellSize);
    }

    public Vector2Int GetGridCoordinates(Vector3 worldPos)
    {
        Vector3 local = worldPos - originR;
        int x = Mathf.FloorToInt(local.x / cellSize);
        int z = Mathf.FloorToInt(local.z / cellSize);
        return new Vector2Int(x, z);
    }

    public bool IsInBounds(int x, int z)
    {
        return x >= 0 && z >= 0 && x < width && z < height;
    }
}
