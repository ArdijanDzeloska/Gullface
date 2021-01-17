using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public static class GlobalScript
{
    private static int lives = 3;
    private static float distance = 0;
    private static bool won;
    private static string name = "Unknown";
    //private static string filepath = Application.dataPath + "/DataAnalytics/logNotStarted.txt";
    //private static string filepath2 = Application.dataPath + "/DataAnalytics/logNotStarted.txt";
    //private static string filepath3 = Application.dataPath + "/DataAnalytics/logNotStarted.txt";

    public static int Lives
    {
        get
        {
            return lives;
        }
        set
        {
            lives = value;
        }
    }

    public static float Distance
    {
        get
        {
            return distance;
        }
        set
        {
            distance = value;
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

    public static string Name
    {
        get
        {
            return name;
        }
        set
        {
            name = value;
        }
    }

    //public static string Filepath
    //{
    //    get
    //    {
    //        return filepath;
    //    }
    //    set
    //    {
    //        filepath = value;
    //    }
    //}

    //public static string Filepath2
    //{
    //    get
    //   {
    //        return filepath2;
    //    }
    //    set
    //    {
    //        filepath2 = value;
    //    }
    //}

    //public static string Filepath3
    //{
    //    get
    //    {
    //        return filepath3;
    //    }
    //    set
    //    {
    //        filepath3 = value;
    //    }
    //}
}