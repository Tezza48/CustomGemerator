class Room
{
    private int x, y, width, height;

    #region Properties
    public int Height { get { return height; } }

    public int Width { get { return width; } }

    public int X { get { return x; } }

    public int Y { get { return y; } }
    #endregion

    public Room(int _x, int _y, int _width, int _height)
    {
        x = _x;
        y = _y;
        width = _width;
        height = _height;
    }

    public bool RoomContains(int _x, int _y)
    {
        if (_x >= x && _x <= width)
        {
            if (_y >= y && _y <= height)
            {
                return true;
            }
        }
        return false;
    }

    public bool RoomCollides(Room _checkRoom)
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
}