using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room
{
    public RectInt Bounds;
    public Room(Vector2Int location, Vector2Int size)
    {
        Bounds = new RectInt(location, size);
    }

    public static bool Intersect(Room a, Room b)
    {
        return !((a.Bounds.position.x >= (b.Bounds.position.x + b.Bounds.size.x)) || ((a.Bounds.position.x + a.Bounds.size.x) <= b.Bounds.position.x)
            || (a.Bounds.position.y >= (b.Bounds.position.y + b.Bounds.size.y)) || ((a.Bounds.position.y + a.Bounds.size.y) <= b.Bounds.position.y));
    }

    public bool IsInRoom(Vector3Int position)
    {
        return (Bounds.position.x <= position.x) && (Bounds.position.x + Bounds.size.x >= position.x) &&
             (Bounds.position.y <= position.z) && (Bounds.position.y + Bounds.size.y >= position.z);
    }
}
