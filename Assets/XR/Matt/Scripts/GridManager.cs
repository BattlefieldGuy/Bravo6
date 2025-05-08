using UnityEngine;

public class GridManager : MonoBehaviour
{
    public int Width = 10;
    public int Height = 10;
    public float CellSize = 1f;
    public Vector3 Origin = Vector3.zero;

    public Vector3 GetWorldPosition(int x, int y)
    {
        return Origin + new Vector3(x * CellSize, 0, y * CellSize);
    }

    public Vector2Int GetGridCoordinates(Vector3 worldPos)
    {
        Vector3 local = worldPos - Origin;
        int x = Mathf.FloorToInt(local.x / CellSize);
        int z = Mathf.FloorToInt(local.z / CellSize);
        return new Vector2Int(x, z);
    }

    public bool IsInBounds(int x, int z)
    {
        return x >= 0 && z >= 0 && x < Width && z < Height;
    }

}