using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = System.Random;

public class LevelGenerator : MonoBehaviour
{
    public int RoomAmount;

    public Vector2Int Size;
    public Vector2Int RoomMaxSize;

    public GameObject CubePrefab;

    List<Room> Rooms;
    Random RandomNum;

    // Start is called before the first frame update
    void Start()
    {
        GenerateDungeon();
    }

    // Update is called once per frame
    void Update()
    {

    }

    void GenerateDungeon()
    {
        RandomNum = new Random(2);
        Rooms = new List<Room>();

        PlaceRooms();
    }

    void PlaceRooms()
    {
        // Generate Random Rooms
        for (int i = 0; i < RoomAmount; i++)
        {
            Vector2Int location = new Vector2Int(
                RandomNum.Next(0, Size.x),
                RandomNum.Next(0, Size.y)
                );

            Vector2Int roomSize = new Vector2Int(
                RandomNum.Next(1, RoomMaxSize.x),
                RandomNum.Next(1, RoomMaxSize.y)
                );

            Room newRoom = new Room(location, roomSize);

            Rooms.Add(newRoom);
            PlaceRoom(newRoom.Bounds.position, newRoom.Bounds.size);
        }
    }

    void PlaceRoom(Vector2Int location, Vector2Int size)
    {
        GameObject go = Instantiate(CubePrefab, new Vector3(location.x, location.y, 0), Quaternion.identity);
        go.GetComponent<Transform>().localScale = new Vector3(size.x, size.y, 0.01f);
        
        // Get Assets
    }
}
