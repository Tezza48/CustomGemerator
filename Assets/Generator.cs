using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class Generator : MonoBehaviour {
    [Header("Generator Properties")]
    private const int WIDTH = 20, HEIGHT = 20;
    private const int TILE_SIZE = 40;

    [Header("Room Properties")]
    private int roomsToSpawn = 3;
    private int maxRoomTries = 100;
    private int minRoomSize = 3, maxRoomSize = 10;

    [Header("Tile Prefabs")]
    public GameObject[] CoridoorTiles;
    public GameObject[] RoomTiles;

    private Cell[,] cells;
    private List<Room> Rooms = new List<Room>();

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
        // Rooms.Add(new Room(4, 4, 5, 8));
        // Rooms.Add(new Room(6, 8, 13, 17));

        GenerateRooms();
        /*
        GenerateCoridoors();
        MakeDoors();
        */

        MakeTiles();
    }

    private void MakeTiles()
    {
        Vector3 spawnPos;
        int spawnOrientation = 0;
        GameObject newTile = CoridoorTiles[0];
        Cell currentCell;
        for (int y = 0; y < HEIGHT; y++)
        {
            for (int x = 0; x < WIDTH; x++)
            {
                spawnPos = new Vector3(x * TILE_SIZE, 0, y * TILE_SIZE);
                spawnOrientation = 0;
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
                if (currentRoom != null)
                {
                    if (x == currentRoom.X && y == currentRoom.Y)
                    {
                        newTile = RoomTiles[8];
                        spawnOrientation = 3;
                    }
                    else if (x == currentRoom.X && y == currentRoom.Height)
                    {
                        newTile = RoomTiles[8];
                        spawnOrientation = 0;
                    }
                    else if (x == currentRoom.Width && y == currentRoom.Y)
                    {
                        newTile = RoomTiles[8];
                        spawnOrientation = 2;
                    }
                    else if (x == currentRoom.Width && y == currentRoom.Height)
                    {
                        newTile = RoomTiles[8];
                        spawnOrientation = 1;
                    }
                    else if (x == currentRoom.X)
                    {
                        newTile = RoomTiles[5];
                        spawnOrientation = 3;
                    }
                    else if (x == currentRoom.Width)
                    {
                        newTile = RoomTiles[5];
                        spawnOrientation = 1;
                    }
                    else if (y == currentRoom.Y)
                    {
                        newTile = RoomTiles[5];
                        spawnOrientation = 2;
                    }
                    else if (y == currentRoom.Height)
                    {
                        newTile = RoomTiles[5];
                        spawnOrientation = 0;
                    }
                    else
                    {
                        newTile = RoomTiles[0];
                        spawnOrientation = UnityEngine.Random.Range(0, 4);
                    }
                }
                else
                {
                    switch (currentCell.getNumExits())
                    {
                        case 0:
                            newTile = CoridoorTiles[0];
                            break;
                        case 1:
                            newTile = CoridoorTiles[1];
                            break;
                        case 2:
                            if (currentCell.Exits == 10 || currentCell.Exits == 5)
                            {
                                newTile = CoridoorTiles[3];
                            }
                            else
                            {
                                newTile = CoridoorTiles[2];
                            }
                            break;
                        case 3:
                            newTile = CoridoorTiles[4];
                            break;
                        case 4:
                            newTile = CoridoorTiles[5];
                            break;
                        default:
                            newTile = new GameObject("Error Tile");
                            break;
                    }
                }
                Instantiate(newTile, spawnPos, Quaternion.Euler(0f, 90*spawnOrientation ,0f));
            }
        }
    }

    private void MakeDoors()
    {
        throw new NotImplementedException();
    }

    private void GenerateCoridoors()
    {
        throw new NotImplementedException();
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

                bool isValid = false;
                foreach (Room currentRoom in Rooms)
                {
                    if (currentRoom.RoomCollides(newRoom))
                    {
                        isValid = false;
                        break;
                    }
                    else
                    {
                        isValid = true;
                    }
                }

                if (isValid)
                {
                    Rooms.Add(newRoom);
                }

                tryCounter--;
            }
        }
    }
}
