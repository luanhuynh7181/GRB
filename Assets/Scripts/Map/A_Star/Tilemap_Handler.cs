using System.Collections.Generic;
using System.IO;
using GameConstants;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UIElements;

public class TileMap_Handler : MonoBehaviour
{
    public static TileMap_Handler Instance;
    public TileInfo tileInfo;

    public TileInfo GetTileInfo()
    {
        return tileInfo;
    }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
            return;
        }
        Instance = this;
        //DontDestroyOnLoad(gameObject);
    }

    void GetAllTiles()
    {
        Tilemap tilemap = GetComponent<Tilemap>();

        if (tilemap == null)
        {
            Debug.LogError("Tilemap is not assigned!");
            return;
        }

        BoundsInt bounds = tilemap.cellBounds;

        int xMin = bounds.xMax;
        int xMax = bounds.xMin;
        int yMin = bounds.yMax;
        int yMax = bounds.yMin;
        for (int x = bounds.xMin; x < bounds.xMax; x++)
        {
            for (int y = bounds.yMin; y < bounds.yMax; y++)
            {
                TileBase tile = tilemap.GetTile(new Vector3Int(x, y, 0));

                if (tile != null)
                {
                    xMin = Mathf.Min(xMin, x);
                    xMax = Mathf.Max(xMax, x);
                    yMin = Mathf.Min(yMin, y);
                    yMax = Mathf.Max(yMax, y);
                }
            }
        }

        Vector2Int offset = new Vector2Int(xMin, yMin);
        int[,] grid = new int[xMax - xMin + 1, yMax - yMin + 1];
        tileInfo = new TileInfo(grid, offset);
        for (int x = xMin; x <= xMax; x++)
        {
            for (int y = yMin; y <= yMax; y++)
            {
                Vector3Int cellPosition = new Vector3Int(x, y, 0);
                TileBase tile = tilemap.GetTile(cellPosition);
                grid[x - offset.x, y - offset.y] = tile != null ? 1 : 0;
            }
        }
    }

    void Start()
    {
        if (DevMode.IsOffAStar)
            return;
        GetAllTiles();
    }
}
