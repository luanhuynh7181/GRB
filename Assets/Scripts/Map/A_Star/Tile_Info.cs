using UnityEngine;

public class TileInfo
{
    public int[,] grid { get; }
    public Vector2Int offset { get; }

    public TileInfo(int[,] grid, Vector2Int offset)
    {
        this.grid = grid;
        this.offset = offset;
    }
}
