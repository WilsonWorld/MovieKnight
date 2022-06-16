using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathNode
{
    CustomGrid<PathNode> m_grid;
    public int x, y;

    public int gCost, hCost, fCost;
    public PathNode prevNode;

    public PathNode(CustomGrid<PathNode> grid, int x, int y)
    {
        this.m_grid = grid;
        this.x = x;
        this.y = y;
    }

    public void CalculateFCost()
    {
        fCost = gCost + hCost;
    }

    public override string ToString()
    {
        return x + ", " + y;
    }
}
