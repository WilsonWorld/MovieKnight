using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = System.Random;
using Graphs;

public class LevelGenerator : MonoBehaviour
{
    enum CellType
    {
        None = 0,
        Room,
        Hallway
    }

    public int Seed;
    public int RoomAmount;

    public Vector2Int Size;
    public Vector2Int RoomMaxSize;

    public GameObject CubePrefab;

    public Material OrangeMat;
    public Material BlueMat;

    List<Room> Rooms;
    Random RandomNum;
    Grid2D<CellType> Grid;
    Delaunay2D Delaunay;
    HashSet<Prim.Edge> SelectedEdges;

    void Start()
    {
        Generate();
    }

    void Generate()
    {
        RandomNum = new Random(Seed);
        Grid = new Grid2D<CellType>(Size, Vector2Int.zero);
        Rooms = new List<Room>();

        PlaceRooms();
        Triangulate(); 
        CreateHallways();
        PathfindHallways();
    }

    void PlaceRooms()
    {
        for (int i = 0; i < RoomAmount; i++)
        {
            Vector2Int location = new Vector2Int(
                RandomNum.Next(0, Size.x),
                RandomNum.Next(0, Size.y)
            );

            Vector2Int roomSize = new Vector2Int(
                RandomNum.Next(1, RoomMaxSize.x + 1),
                RandomNum.Next(1, RoomMaxSize.y + 1)
            );

            bool add = true;
            Room newRoom = new Room(location, roomSize);
            Room buffer = new Room(location + new Vector2Int(-1, -1), roomSize + new Vector2Int(2, 2));

            foreach (var room in Rooms)
            {
                if (Room.Intersect(room, buffer))
                {
                    add = false;
                    break;
                }
            }

            if (newRoom.Bounds.xMin < 0 || newRoom.Bounds.xMax >= Size.x
                || newRoom.Bounds.yMin < 0 || newRoom.Bounds.yMax >= Size.y)
            {
                add = false;
            }

            if (add)
            {
                Rooms.Add(newRoom);
                PlaceRoom(newRoom.Bounds.position, newRoom.Bounds.size);

                foreach (var pos in newRoom.Bounds.allPositionsWithin)
                {
                    Grid[pos] = CellType.Room;
                }
            }
        }
    }

    void Triangulate()
    {
        List<Vertex> vertices = new List<Vertex>();

        foreach (var room in Rooms)
        {
            vertices.Add(new Vertex<Room>((Vector2)room.Bounds.position + ((Vector2)room.Bounds.size) / 2, room));
        }

        Delaunay = Delaunay2D.Triangulate(vertices);
    }

    void CreateHallways()
    {
        List<Prim.Edge> edges = new List<Prim.Edge>();

        foreach (var edge in Delaunay.Edges)
        {
            edges.Add(new Prim.Edge(edge.U, edge.V));
        }

        List<Prim.Edge> mst = Prim.MinimumSpanningTree(edges, edges[0].U);

        SelectedEdges = new HashSet<Prim.Edge>(mst);
        var remainingEdges = new HashSet<Prim.Edge>(edges);
        remainingEdges.ExceptWith(SelectedEdges);

        foreach (var edge in remainingEdges)
        {
            if (RandomNum.NextDouble() < 0.125)
            {
                SelectedEdges.Add(edge);
            }
        }
    }

    void PathfindHallways()
    {
        DungeonPathfinder2D aStar = new DungeonPathfinder2D(Size);

        foreach (var edge in SelectedEdges)
        {
            var startRoom = (edge.U as Vertex<Room>).Item;
            var endRoom = (edge.V as Vertex<Room>).Item;

            var startPosf = startRoom.Bounds.center;
            var endPosf = endRoom.Bounds.center;
            var startPos = new Vector2Int((int)startPosf.x, (int)startPosf.y);
            var endPos = new Vector2Int((int)endPosf.x, (int)endPosf.y);

            var path = aStar.FindPath(startPos, endPos, (DungeonPathfinder2D.Node a, DungeonPathfinder2D.Node b) => {
                var pathCost = new DungeonPathfinder2D.PathCost();

                pathCost.cost = Vector2Int.Distance(b.Position, endPos);    //heuristic

                if (Grid[b.Position] == CellType.Room)
                {
                    pathCost.cost += 10;
                }
                else if (Grid[b.Position] == CellType.None)
                {
                    pathCost.cost += 5;
                }
                else if (Grid[b.Position] == CellType.Hallway)
                {
                    pathCost.cost += 1;
                }

                pathCost.traversable = true;

                return pathCost;
            });

            if (path != null)
            {
                for (int i = 0; i < path.Count; i++)
                {
                    var current = path[i];

                    if (Grid[current] == CellType.None)
                    {
                        Grid[current] = CellType.Hallway;
                    }

                    if (i > 0)
                    {
                        var prev = path[i - 1];

                        var delta = current - prev;
                    }
                }

                foreach (var pos in path)
                {
                    if (Grid[pos] == CellType.Hallway)
                    {
                        PlaceHallway(pos);
                    }
                }
            }
        }
    }

    void PlaceCube(Vector2Int location, Vector2Int size, Material material)
    {
        GameObject go = Instantiate(CubePrefab, new Vector3(location.x, 0, location.y), Quaternion.identity);
        go.GetComponent<Transform>().localScale = new Vector3(size.x, 1, size.y);
        go.GetComponent<MeshRenderer>().material = material;
    }

    void PlaceRoom(Vector2Int location, Vector2Int size)
    {
        PlaceCube(location, size, OrangeMat);
    }

    void PlaceHallway(Vector2Int location)
    {
        PlaceCube(location, new Vector2Int(1, 1), BlueMat);
    }
}
