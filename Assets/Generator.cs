using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class Generator : MonoBehaviour {
    [Header("Generator Properties")]
    public int width = 20, height = 20;
    public int tileSize = 40;

    [Header("Room Properties")]
    public int roomsToSpawn = 3;
    public int minRoomSize = 3, maxRoomSize = 10;

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
        cells = new Cell[width, height];
        for (int y = 0; y < height; y++)
        {
            for (int x = 0; x < width; x++)
            {
                cells[x, y] = new Cell();
            }
        }
        Rooms.Add(new Room(4, 4, 7, 7));
        /*
        GenerateRooms();
        GenerateCoridoors();
        MakeDoors();
        */

        MakeTiles();
    }

    private void MakeTiles()
    {
        Vector3 spawnPos;
        int SpawnOrientation;
        GameObject newTile;
        Cell currentCell;
        for (int y = 0; y < height; y++)
        {
            for (int x = 0; x < width; x++)
            {
                spawnPos = new Vector3(x * tileSize, 0, y * tileSize);
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
                    if (y == currentRoom.Y)
                    {
                        if (x == currentRoom.X)
                        {
                            newTile = RoomTiles[8];
                            SpawnOrientation = 0;
                        }
                        else if (x == currentRoom.Width)
                        {
                            newTile = RoomTiles[8];
                            SpawnOrientation = 3;
                        }
                        else
                        {
                            newTile = RoomTiles[5];
                            SpawnOrientation = 
                        }
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
                Instantiate(newTile, spawnPos, Quaternion.identity);
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
        throw new NotImplementedException();
    }
}
