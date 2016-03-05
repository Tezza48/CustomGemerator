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
    Corner = 8, Centre = 0, Edge = 5
}

public class Generator : MonoBehaviour {
    [Header("Generator Properties")]
    private const int WIDTH = 20, HEIGHT = 20;
    private const int TILE_SIZE = 40;

    [Header("Room Properties")]
    private int roomsToSpawn = 5;
    private int maxRoomTries = 10;
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
        // Rooms.Add(new Room(4, 4, 3, 3));

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
                    RoomTiles tile = currentRoom.CheckPosition(ref spawnOrientation, x, y);
                    newTile = RoomTiles[(int)tile];
                }
                else
                {
                    switch (currentCell.getNumExits())
                    {
                        case 0:
                            newTile = CoridoorTiles[(int)MazeTiles.Filler];
                            break;
                        case 1:
                            newTile = CoridoorTiles[(int)MazeTiles.Deadend];
                            break;
                        case 2:
                            if (currentCell.Exits == 10 || currentCell.Exits == 5)
                            {
                                newTile = CoridoorTiles[(int)MazeTiles.Straight];
                            }
                            else
                            {
                                newTile = CoridoorTiles[(int)MazeTiles.Corner];
                            }
                            break;
                        case 3:
                            newTile = CoridoorTiles[(int)MazeTiles.Junction];
                            break;
                        case 4:
                            newTile = CoridoorTiles[(int)MazeTiles.Cross];
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

                if (xPos + roomWidth > WIDTH || yPos + roomHeight > HEIGHT)
                {
                    continue;
                }

                bool isValid = true;
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
                    Debug.Log(isValid.ToString() + newRoom.X + ", "  + newRoom.Y + ", " + newRoom.Width + ", " + newRoom.Height);
                    break;
                }

                tryCounter--;
            }
        }
    }
}
