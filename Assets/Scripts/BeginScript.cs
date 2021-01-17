using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class BeginScript
{
    private static int lifes = 3;
    private static bool won;

    public static int Lifes
    {
        get
        {
            return lifes;
        }
        set
        {
            lifes = value;
        }
    }

    public static bool Won
    {
        get
        {
            return won;
        }
        set
        {
            won = value;
        }
    }
}