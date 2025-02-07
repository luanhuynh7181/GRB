using System;
using UnityEngine;

public class Node_Tile
{
    // approx cost
    public float F => G + H;

    public float G { get; set; }

    // cur cost
    public float H { get; set; }

    public Node_Tile Parent { get; set; }
    public int X { get; set; }
    public int Y { get; set; }

    public Node_Tile(int x, int y)
    {
        X = x;
        Y = y;
    }

    public Node_Tile(float x, float y)
    {
        X = (int)Math.Floor(x);
        Y = (int)Math.Floor(y);
    }

    public override bool Equals(object obj)
    {
        if (obj is null)
            return false;
        if (obj is Node_Tile other)
        {
            return X == other.X && Y == other.Y;
        }
        return false;
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(X, Y);
    }

    public Vector2 GetPosition()
    {
        return new Vector2(X + 0.5f, Y + 0.5f);
    }

    public override string ToString()
    {
        return $"X: {X}, Y: {Y}";
    }
}
