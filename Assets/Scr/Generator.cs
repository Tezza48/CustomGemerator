using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

enum MazeTiles
{
    Filler, Deadend, Corner, Straight, Junction, Cross
}

enum RoomTiles
{
    Centre,
    Edge,
    Corner,
    DoorEdge,
    UNDEFINED
}

public class Generator : MonoBehaviour {
    [Header("Generator Fields")]
    [SerializeField][Range(5, 50)] private int WIDTH = 40;
    [SerializeField][Range(5, 50)] private int HEIGHT = 40;
    private const int TILE_SIZE = 40;

    [Header("Room Fields")]
    [SerializeField][Range(5, 20)] private int roomsToSpawn = 13;
    private int maxRoomTries = 100;
    private int minRoomSize = 3;
    [SerializeField][Range(8, 20)] private int maxRoomSize = 8;

    [Header("Tile Fields")]
    public GameObject[] CoridoorTilePrefabs;
    public GameObject[] RoomTilePrefabs;

    private Cell[,] cells;
    private List<Room> Rooms = new List<Room>();
    private List<Line> hallways;

    // Use this for initialization
    void Start () {
        Generate();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void Generate()
    {
        cells = new Cell[WIDTH, HEIGHT];
        for (int y = 0; y < HEIGHT; y++)
        {
            for (int x = 0; x < WIDTH; x++)
            {
                cells[x, y] = new Cell();
            }
        }
        // Rooms.Add(new Room(4, 4, 3, 3));

        GenerateRooms();
        GenerateCoridoors(ref Rooms, out hallways);
<<<<<<< HEAD

=======
        SetCoridoorCells();
>>>>>>> parent of 59ae842... WORKING! Corners are an issue.
        /*
        SetCorridorCells();
        GenerateMaze();
        MakeDoors();
        */
<<<<<<< HEAD

        //foreach (Room thisRoom in Rooms)
        //{
        //    Debug.Log(thisRoom.Origin.ToString());
        //}
=======
        foreach (Room thisRoom in Rooms)
        {
            Debug.Log(thisRoom.Origin.ToString());
        }
>>>>>>> parent of 59ae842... WORKING! Corners are an issue.
        foreach (Line hallway in hallways)
        {
            Debug.DrawLine(hallway.Origin1v3 * TILE_SIZE, hallway.Origin2v3 * TILE_SIZE, Color.cyan, 1000f, false);
        }
<<<<<<< HEAD

=======
>>>>>>> parent of 59ae842... WORKING! Corners are an issue.
        MakeTiles();
    }

    private void SetCorridorCells()
    {
        foreach (Line hallway in hallways)
        {
            int[] startPos = new int[] { (int)hallway.O1.x, (int)hallway.O1.y };
            int[] endPos = new int[] { (int)hallway.O2.x, (int)hallway.O2.y };

            int dx = endPos[0] - startPos[0];
            int dy = endPos[1] - startPos[1];

            int isPosX = dx > 0 ? 1 : -1;
            int isPosY = dy > 0 ? 1 : -1;
            
            //which way do i go first?

            switch (hallway.getLineStyle())
            {
                case Line.LineStyle.VertFirst:
                    // vertical
                    if (dy > 0) // if y up
                    {
                        for (int y = startPos[1]; y < endPos[1] - 1; y++)
                        {
                            cells[startPos[0], y].Exits |= (int)Exit.Down + (int)Exit.Up;
                        }
                        cells[startPos[0], endPos[1]].Exits |= (int)InverseExit.Up;
                    }
                    else // if y down
                    {
                        for (int y = startPos[1]; y > endPos[1] + 1; y--)
                        {
                            cells[startPos[0], y].Exits |= (int)Exit.Down + (int)Exit.Up;
                        }
                        cells[startPos[0], endPos[1]].Exits |= (int)InverseExit.Down;
                    }
                    // horizontal
                    if (dx > 0) // if x right
                    {
                        cells[startPos[0], endPos[1]].Exits |= (int)InverseExit.Right;
                        for (int x = startPos[0]; x < endPos[0] - 1; x++)
                        {
                            cells[x, endPos[1]].Exits |= (int)Exit.Left + (int)Exit.Right;
                        }
                    }
                    else // if x left
                    {
                        cells[startPos[0], endPos[1]].Exits |= (int)InverseExit.Left;
                        for (int x = startPos[0]; x > endPos[0] + 1; x--)
                        {
                            cells[x, endPos[1]].Exits |= (int)Exit.Left + (int)Exit.Right;
                        }
                    }
                    break;
                case Line.LineStyle.HorizFirst:
                    if (dx > 0) // if x right
                    {
                        for (int x = startPos[0]; x < endPos[0] - 1; x++)
                        {
                            cells[x, startPos[1]].Exits |= (int)Exit.Left + (int)Exit.Right;
                        }
                        cells[endPos[0], startPos[1]].Exits |= (int)InverseExit.Right;
                    }
                    else // if x left
                    {
                        for (int x = startPos[0]; x > endPos[0] + 1; x--)
                        {
                            cells[x, startPos[1]].Exits |= (int)Exit.Left + (int)Exit.Right;
                        }
                        cells[endPos[0], startPos[1]].Exits |= (int)InverseExit.Left;
                    }
                    if (dy > 0) // if y up
                    {
                        cells[endPos[0], startPos[1]].Exits |= (int)InverseExit.Up;
                        for (int y = startPos[1]; y < endPos[1] - 1; y++)
                        {
                            cells[endPos[0], y].Exits |= (int)Exit.Up + (int)Exit.Down;
                        }
                    }
                    else// if y down
                    {
                        cells[endPos[0], startPos[1]].Exits |= (int)InverseExit.Down;
                        for (int y = startPos[1]; y > endPos[1] + 1; y--)
                        {
                            cells[endPos[1], y].Exits |= (int)Exit.Up + (int)Exit.Down;
                        }
                    }
                    break;
                default:
                    break;
            }
        }
    }

    private void MakeTiles()
    {
        for (int y = 0; y < HEIGHT; y++)
        {
            for (int x = 0; x < WIDTH; x++)
            {
<<<<<<< HEAD
                foreach (Room currentRoom in Rooms)
=======
                spawnPos = new Vector3(x * TILE_SIZE, 0, y * TILE_SIZE);
                spawnOrientation = 0;
                RoomTiles tile = RoomTiles.UNDEFINED;
                newTile = null;
                currentCell = cells[x, y];
                Room currentRoom = null;
                foreach (Room checkRoom in Rooms)
                {
                    if (checkRoom.RoomContains(x, y))
                    {
                        currentRoom = checkRoom;
                        break;
                    }
                }


                // currentRoom = null; // REMOVE THIS TO GET ROOMS


                if (currentRoom != null)
                {
                    tile = currentRoom.CheckPosition(ref spawnOrientation, x, y);
                    newTile = RoomTilePrefabs[(int)tile];
                    if (tile == RoomTiles.Edge && x > 0 && y > 0 && x < WIDTH - 1 && y < HEIGHT - 1)
                    {
                        switch (spawnOrientation)
                        {
                            case 0:
                                if ((cells[x, y+1].Exits & (int) Exit.Left) == (int) Exit.Left)
                                {
                                    tile = RoomTiles.DoorEdge;
                                }
                                break;
                            case 2:
                                if ((cells[x, y - 1].Exits & (int)Exit.Right) == (int)Exit.Right)
                                {
                                    tile = RoomTiles.DoorEdge;
                                }
                                break;
                            case 3:
                                if ((cells[x - 1, y].Exits & (int)Exit.Up) == (int)Exit.Up)
                                {
                                    tile = RoomTiles.DoorEdge;
                                }
                                break;
                            case 4:
                                if ((cells[x + 1, y].Exits & (int)Exit.Down) == (int)Exit.Down)
                                {
                                    tile = RoomTiles.DoorEdge;
                                }
                                break;

                            default:
                                break;
                        }
                    }
                }
                else
>>>>>>> parent of 59ae842... WORKING! Corners are an issue.
                {
                    if (currentRoom.RoomRect.Contains( new Vector2(x, y) ) )
                    {
<<<<<<< HEAD
                        Instantiate(CoridoorTilePrefabs[0], new Vector3(x * TILE_SIZE, 0, y * TILE_SIZE), Quaternion.identity);
=======
                        case 0:
                            newTile = CoridoorTilePrefabs[(int)MazeTiles.Filler];
                            break;
                        case 1:
                            newTile = CoridoorTilePrefabs[(int)MazeTiles.Deadend];
                            break;
                        case 2:
                            if (currentCell.Exits == 10 || currentCell.Exits == 5)
                            {
                                newTile = CoridoorTilePrefabs[(int)MazeTiles.Straight];

                                // if it's up / down, make the orientation = 1
                                spawnOrientation = (currentCell.Exits & (int)Exit.Up) == (int)Exit.Up ? 0 : 1;
                            }
                            else
                            {
                                newTile = CoridoorTilePrefabs[(int)MazeTiles.Corner];
                            }
                            break;
                        case 3:
                            newTile = CoridoorTilePrefabs[(int)MazeTiles.Junction];
                            break;
                        case 4:
                            newTile = CoridoorTilePrefabs[(int)MazeTiles.Cross];
                            break;
                        default:
                            newTile = new GameObject("Error Tile");
                            break;
>>>>>>> parent of 59ae842... WORKING! Corners are an issue.
                    }
                }
            }
        }
    }

    private void MakeDoors()
    {
        throw new NotImplementedException();
    }

    private void GenerateCoridoors(ref List<Room> _Rooms, out List<Line> _hallways)
    {
        _hallways = new List<Line>();
        // first room is starting point so dont do anything
        for (int i = 1; i < _Rooms.Count; i++)
        {
            // as this is the second room, we assume it's the closest room yet
            int closestRoom = i;
            float closestDistance = Vector2.Distance(_Rooms[i-1].Origin, _Rooms[i].Origin);
            // check distance to all other rooms
            for (int j = i + 1; j < _Rooms.Count; j++)
            {
                float currentDistance = Vector2.Distance(_Rooms[j].Origin, _Rooms[i].Origin);
                if (currentDistance < closestDistance)
                {
                    closestDistance = currentDistance;
                    closestRoom = j;
                }
            }
            // make the closest room the second in the list
            if(closestRoom != i)
            {
                SwapRooms(ref _Rooms, i, closestRoom);
            }
            // repeat for all of the rooms
        }
        // the rooms are now sorted in an order
        // now make hallways between the rooms
        for (int i = 1; i < _Rooms.Count; i++)
        {
            _hallways.Add(new Line(_Rooms[i - 1].Origin, _Rooms[i].Origin));
        }
    }

    private void SwapRooms(ref List<Room> _Rooms, int i, int closestRoom)
    {
        Room swapRoom = _Rooms[i];
        _Rooms[i] = _Rooms[closestRoom];
        _Rooms[closestRoom] = swapRoom;
    }

    private void GenerateRooms()
    {
        for (int i = 0; i < roomsToSpawn; i++)
        {
            int tryCounter = maxRoomTries;
            while (tryCounter > 0)
            {
                int xPos = UnityEngine.Random.Range(0, WIDTH - minRoomSize);
                int yPos = UnityEngine.Random.Range(0, HEIGHT - minRoomSize);
                int roomWidth = UnityEngine.Random.Range(minRoomSize, maxRoomSize);
                int roomHeight = UnityEngine.Random.Range(minRoomSize, maxRoomSize);

                Room newRoom = new Room(xPos, yPos, roomWidth, roomHeight);

                if (xPos + roomWidth + 1 > WIDTH || yPos + roomHeight + 1 > HEIGHT)
                {
                    continue;
                }

                bool isValid = true;
                foreach (Room currentRoom in Rooms)
                {
                    if (Room.RoomsOverlap(currentRoom, newRoom))
                    {
                        isValid = false;
                        break;
                    }
                }

                // Debug.Log(isValid.ToString() + newRoom.X + ", "  + newRoom.Y + ", " + (newRoom.Width + newRoom.X).ToString() + ", " + (newRoom.Height + newRoom.Y).ToString());

                if (isValid)
                {
                    Rooms.Add(newRoom);
                    break;
                }

                tryCounter--;
            }
        }
    }
}
