using System;
using System.Collections;

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
        // return the number of exits
        switch (exits)
        {
            case 1:
            case 2:
            case 4:
            case 8:
                return 1;

            case 3:
            case 5:
            case 9:
            case 6:
            case 10:
            case 12:
                return 2;

            case 7:
            case 11:
            case 14:
                return 3;

            case 15:
                return 4;

            default:
                return 0;
        }
    }
}