using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CodeMonkey.Utils;

public class PathTest : MonoBehaviour
{
    Pathfinding m_pathfinding;

    private void Start()
    {
        m_pathfinding = new Pathfinding(10, 10);
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector2 mouseWorldPos = UtilsClass.GetMouseWorldPosition();
            m_pathfinding.GetGrid().GetXY(mouseWorldPos, out int x, out int y);
            List<PathNode> path = m_pathfinding.FindPath(0, 0, x, y);
            if (path != null)
            {
                for (int i = 0; i < path.Count -1; i++)
                {
                    Debug.DrawLine(new Vector3(path[i].x, path[i].y) * 10.0f + Vector3.one * 5.0f, new Vector3(path[i + 1].x, path[i + 1].y) * 10.0f + Vector3.one * 5.0f, Color.green, 10.0f);
                }
            }
        }
    }
}
