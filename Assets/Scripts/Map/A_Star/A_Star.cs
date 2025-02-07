
using GameConstants;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


public class AStar
{
    private static readonly Node_Around[] NextToNodes = new Node_Around[]
    {
        new Node_Around(0, 1),
        new Node_Around(1, 0),
        new Node_Around(0, -1),
        new Node_Around(-1, 0)
    };
    private static readonly Node_Around[] DiagonalNodes = new Node_Around[]
 {
     new Node_Around(1, 1, new List<Node_Around> {NextToNodes[0], NextToNodes[1]  }),
     new Node_Around(1, -1, new List<Node_Around> {NextToNodes[1], NextToNodes[2]  }),
     new Node_Around(-1, -1, new List<Node_Around> {NextToNodes[2], NextToNodes[3]  }),
     new Node_Around(-1, 1, new List<Node_Around> {NextToNodes[3], NextToNodes[0]  })
 };

    public static List<Node_Tile> FindPath(Node_Tile start, Node_Tile goal)
    {
        TileInfo tileInfo = TileMap_Handler.Instance.GetTileInfo();
        List<Node_Tile> path = FindPath(tileInfo, start, goal);
        if (path == null || path.Count == 0) return null;
        path.RemoveAt(0);
        return path;
    }
    public static List<Node_Tile> FindPath(TileInfo tileInfo, Node_Tile start, Node_Tile goal)
    {
        if (DevMode.IsOffAStar) return null;

        var openList = new List<Node_Tile>();
        var closedList = new HashSet<Node_Tile>();
        openList.Add(start);

        List<Node_Around> aroundNodes = NextToNodes.Concat(DiagonalNodes).ToList();
        while (openList.Count > 0)
        {
            Node_Tile current = GetNodeWithLowestF(openList);
            if (current.X == goal.X && current.Y == goal.Y)
                return ReconstructPath(current);

            openList.Remove(current);
            closedList.Add(current);

            foreach (Node_Around node in aroundNodes)
            {
                int newX = current.X + node.X;
                int newY = current.Y + node.Y;

                if (!IsValid(tileInfo, newX, newY)) continue;
                if (!IsValidPrerequisite(tileInfo, current, node.prerequisite)) continue;
                if (closedList.Contains(new Node_Tile(newX, newY))) continue;

                var neighbor = new Node_Tile(newX, newY)
                {
                    G = current.G + node.Cost,
                    H = Math.Abs(newX - goal.X) + Math.Abs(newY - goal.Y),
                    Parent = current
                };

                var existingNode = openList.Find(n => n.X == neighbor.X && n.Y == neighbor.Y);
                if (existingNode == null)
                {
                    openList.Add(neighbor);
                }
                else if (neighbor.G < existingNode.G)
                {
                    existingNode.G = neighbor.G;
                    existingNode.Parent = current;
                }
            }
        }

        return null;
    }

    private static Node_Tile GetNodeWithLowestF(List<Node_Tile> nodes)
    {
        Node_Tile lowestFNode = nodes[0];
        foreach (var node in nodes)
        {
            if (node.F < lowestFNode.F)
                lowestFNode = node;
        }
        return lowestFNode;
    }

    private static bool IsValid(TileInfo tileInfo, int x, int y)
    {
        var grid = tileInfo.grid;
        x -= tileInfo.offset.x;
        y -= tileInfo.offset.y;
        return x >= 0 && y >= 0 && x < grid.GetLength(0) && y < grid.GetLength(1) && grid[x, y] != 0;
    }

    private static bool IsValidPrerequisite(TileInfo tileInfo, Node_Tile cur, List<Node_Around> prerequisite)
    {
        if (prerequisite == null) return true;
        foreach (Node_Around node in prerequisite)
        {
            if (!IsValid(tileInfo, node.X + cur.X, node.Y + cur.Y)) return false;
        }
        return true;
    }

    private static List<Node_Tile> ReconstructPath(Node_Tile current)
    {
        var path = new List<Node_Tile>();
        while (current != null)
        {
            path.Add(current);
            current = current.Parent;
        }
        path.Reverse();
        return path;
    }
}
