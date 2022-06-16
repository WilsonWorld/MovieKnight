using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room : MonoBehaviour
{
    public RectInt Bounds;
    public Room(Vector2Int location, Vector2Int size)
    {
        Bounds = new RectInt(location, size);
    }
}
