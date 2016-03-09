using UnityEngine;

class Room
{

    private Rect roomRect;

    #region Properties
    public int Height { get { return (int)roomRect.height; } }

    public int Width { get { return (int)roomRect.width; } }

    public int X { get { return (int)roomRect.x; } }

    public int Y { get { return (int)roomRect.y; } }

    public Rect RoomRect { get { return roomRect;  } }

    public Vector2 Origin { get { return roomRect.center; } }
    #endregion

    public Room(int _x, int _y, int _width, int _height)
    {
        roomRect = new Rect(_x, _y, _width, _height);
    }

    public bool RoomContains(int _x, int _y)
    {
        return roomRect.Contains(new Vector2(_x, _y));
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

    /* public bool DoesOverlap(Room _checkRoom)
    {

        bool bottomLeft = RoomContains(_checkRoom.X, _checkRoom.Y);
        bool bottomRight = RoomContains(_checkRoom.Width, _checkRoom.Y);
        bool topLeft = RoomContains(_checkRoom.X, _checkRoom.Height);
        bool topRight = RoomContains(_checkRoom.Width, _checkRoom.Height);

        if (bottomLeft || bottomRight || topLeft || topRight)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    */
}