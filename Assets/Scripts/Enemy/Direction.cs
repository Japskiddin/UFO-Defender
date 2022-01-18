using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Direction
{
    public const int DIRECTION_LEFT = 0;
    public const int DIRECTION_RIGHT = 1;
    public const int DIRECTION_TOP = 2;
    public const int DIRECTION_BOTTOM = 3;

    private int _x, _y;

    public int X
    {
        get
        {
            return _x;
        }

        set
        {
            _x = value;
        }
    }

    public int Y
    {
        get
        {
            return _y;
        }

        set
        {
            _y = value;
        }
    }
}