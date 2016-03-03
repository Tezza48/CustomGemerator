enum Exit { NONE = 0, Up = 1, Right = 2, Down = 4, Left = 8 }
enum InverseExit { Up = 4, Right = 8, Down = 1, Left = 2 }

class Cell
{
    private int exits;

    public Cell()
    {
        exits = 0;
    }

    public int Exits
    {
        get
        {
            return exits;
        }

        set
        {
            exits = value;
        }
    }

    public int getNumExits()
    {
        int _exitCount = 0;

        _exitCount += exits & (int)Exit.Up;
        _exitCount += exits & (int)Exit.Right;
        _exitCount += exits & (int)Exit.Down;
        _exitCount += exits & (int)Exit.Left;

        return _exitCount;
    }
}