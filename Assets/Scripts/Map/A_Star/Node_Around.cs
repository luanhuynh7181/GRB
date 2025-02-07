using System;
using System.Collections.Generic;

public class Node_Around
{
    public float Cost { get; set; }
    public List<Node_Around> prerequisite { get; set; }
    public int X { get; set; }
    public int Y { get; set; }

    public Node_Around(int x, int y, List<Node_Around> prerequisite = null)
    {
        X = x;
        Y = y;
        this.prerequisite = prerequisite ?? new List<Node_Around>();
        Cost = prerequisite == null ? 1 : MathF.Sqrt(2);
    }
}
