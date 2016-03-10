using UnityEngine;

class Room
{

    private Rect rect;

    #region Properties
    public int Height { get { return (int)rect.height; } }

    public int Width { get { return (int)rect.width; } }

    public int X { get { return (int)rect.x; } }

    public int Y { get { return (int)rect.y; } }

    public Rect RoomRect { get { return rect;  } }

    public Vector2 Origin { get { return rect.center; } }
    #endregion

    public Room(int _x, int _y, int _width, int _height)
    {
        rect = new Rect(_x, _y, _width, _height);
    }

    public bool Contains(int _x, int _y)
    {
        return rect.Contains(new Vector2(_x, _y));
    }

    public RoomTiles CheckPosition(ref int orientation, int _x, int _y)
    {
        RoomTiles tile = RoomTiles.Centre;
        // if corner
        if (_x == X && _y == Y)
        {
            orientation = 3;
            return RoomTiles.Corner;
        }
        else if (_x == X + Width && _y == Y)
        {
            orientation = 2;
            return RoomTiles.Corner;
        }
        else if (_x == X && _y == Y + Height)
        {
            orientation = 0;
            return RoomTiles.Corner;
        }
        else if (_x == X + Width && _y == Y + Height)
        {
            orientation = 1;
            return RoomTiles.Corner;
        }

        // if edge
        else if (_x == X)
        {
            orientation = 3;
            return RoomTiles.Edge;
        }
        else if (_y == Y)
        {
            orientation = 2;
            return RoomTiles.Edge;
        }
        else if (_x == X + Width)
        {
            orientation = 1;
            return RoomTiles.Edge;
        }
        else if (_y == Y + Height)
        {
            orientation = 0;
            return RoomTiles.Edge;
        }
        //else centre
        orientation = UnityEngine.Random.Range(0, 4);
        return tile;
    }
    public static bool RoomsOverlap(Room roomA, Room roomB)
    {
        roomA.rect.x--;
        roomA.rect.y--;
        roomA.rect.width++;
        roomA.rect.height++;

        roomB.rect.x--;
        roomB.rect.y--;
        roomB.rect.width++;
        roomB.rect.height++;

        return roomA.rect.Overlaps(roomB.rect);
    }
}