using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pathfinding
{
    const int MOVE_STRAIGHT_COST = 10;
    const int MOVE_DIAGONAL_COST = 14;

    CustomGrid<PathNode> m_grid;
    List<PathNode> openList;
    List<PathNode> closedList;

    public Pathfinding(int width, int height)
    {
        Vector2 posOffset = new Vector2(0, 0);
        m_grid = new CustomGrid<PathNode>(width, height, 10.0f, posOffset, (CustomGrid<PathNode> g, int x, int y) => new PathNode(g, x, y));
    }

    public CustomGrid<PathNode> GetGrid()
    {
        return m_grid;
    }

    public List<PathNode> FindPath(int startX, int startY, int endX, int endY)
    {
        PathNode startNode = m_grid.GetGridObject(startX, startY);
        PathNode endNode = m_grid.GetGridObject(endX, endY);
        openList = new List<PathNode> { startNode };
        closedList = new List<PathNode>();

        for (int x = 0; x < m_grid.GetWidth(); x++) {
            for (int y = 0; y < m_grid.GetHeight(); y++) {
                PathNode node = m_grid.GetGridObject(x, y);
                node.gCost = int.MaxValue;
                node.CalculateFCost();
                node.prevNode = null;
            }
        }

        startNode.gCost = 0;
        startNode.hCost = CalculateDistanceCost(startNode, endNode);
        startNode.CalculateFCost();

        while (openList.Count > 0)  {
            PathNode currentNode = GetLowestFCostNode(openList);
            if (currentNode == endNode)
                return CalculatePath(endNode);

            openList.Remove(currentNode);
            closedList.Add(currentNode);

            foreach (PathNode neighbourNode in GetNeighbourList(currentNode)) {
                if (closedList.Contains(neighbourNode))
                    continue;

                int tentativeGCost = currentNode.gCost + CalculateDistanceCost(currentNode, neighbourNode);
                if (tentativeGCost < neighbourNode.gCost) {
                    neighbourNode.prevNode = currentNode;
                    neighbourNode.gCost = tentativeGCost;
                    neighbourNode.hCost = CalculateDistanceCost(neighbourNode, endNode);
                    neighbourNode.CalculateFCost();

                    if (!openList.Contains(neighbourNode)) {
                        openList.Add(neighbourNode);
                    }
                }
            }
        }

        // Out of nodes in the openList
        return null;
    }

    List<PathNode> GetNeighbourList(PathNode currentNode)
    {
        List<PathNode> neighbourList = new List<PathNode>();

        if (currentNode.x - 1 >= 0) {
            neighbourList.Add(GetNode(currentNode.x - 1, currentNode.y));           // Left
            if (currentNode.y - 1 >= 0)
                neighbourList.Add(GetNode(currentNode.x - 1, currentNode.y - 1));   // Left Down
            if (currentNode.y + 1 < m_grid.GetHeight())
                neighbourList.Add(GetNode(currentNode.x - 1, currentNode.y + 1));   // Left Up
        }

        if (currentNode.x + 1 < m_grid.GetWidth()) {
            neighbourList.Add(GetNode(currentNode.x + 1, currentNode.y));           // Right
            if (currentNode.y - 1 >= 0)
                neighbourList.Add(GetNode(currentNode.x + 1, currentNode.y - 1));   // Right Down
            if (currentNode.y + 1 < m_grid.GetHeight())
                neighbourList.Add(GetNode(currentNode.x + 1, currentNode.y + 1));   // Right Up
        }

        //Down
        if (currentNode.y - 1 >= 0)
            neighbourList.Add(GetNode(currentNode.x, currentNode.y - 1));

        // Up
        if (currentNode.y + 1 < m_grid.GetHeight())
            neighbourList.Add(GetNode(currentNode.x, currentNode.y + 1));

        return neighbourList;
    }

    PathNode GetNode(int x, int y)
    {
        return m_grid.GetGridObject(x, y);
    }

    List<PathNode> CalculatePath(PathNode endNode)
    {
        List<PathNode> path = new List<PathNode>();
        path.Add(endNode);
        PathNode currentNode = endNode;
        while (currentNode.prevNode != null)
        {
            path.Add(currentNode.prevNode);
            currentNode = currentNode.prevNode;
        }

        path.Reverse();
        return path;
    }

    int CalculateDistanceCost(PathNode a, PathNode b)
    {
        int xDist = Mathf.Abs(a.x - b.x);
        int yDist = Mathf.Abs(a.y - b.y);
        int remaining = Mathf.Abs(xDist - yDist);
        return MOVE_DIAGONAL_COST * Mathf.Min(xDist, yDist) + MOVE_STRAIGHT_COST * remaining;
    }

    PathNode GetLowestFCostNode(List<PathNode> pathNodeList)
    {
        PathNode lowestFCostNode = pathNodeList[0];
        for (int i = 1; i < pathNodeList.Count; i++) {
            if (pathNodeList[i].fCost < lowestFCostNode.fCost)
                lowestFCostNode = pathNodeList[i];
        }

        return lowestFCostNode;
    }
}
