using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class StartScript : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {
        CreateLog();
        GlobalScript.Won = false;
    }

    void CreateLog()
    {
        //int filenumber = 0;
        //string path = Application.dataPath + "/DataAnalytics/log" + filenumber.ToString() + ".txt";
        //while (File.Exists(path))
        //{
        //    filenumber += 1;
        //    path = Application.dataPath + "/DataAnalytics/log" + filenumber.ToString() + ".txt";
        //}
        //
        // General log
        //GlobalScript.Filepath = path;
        //File.WriteAllText(path, "Start log \n");
        //string content = "Login date and time: " + System.DateTime.Now + "\n\n";
        //File.AppendAllText(path, content);

        // Distance to Gillface
        //string path2 = Application.dataPath + "/DataAnalytics/log" + filenumber.ToString() + "DistanceToGillfaceLog" + ".txt";
        //GlobalScript.Filepath2 = path2;
        //File.WriteAllText(path2, "Start Distance to Gillface log \n");
        //string content2 = "Login date and time: " + System.DateTime.Now + "\n\n";
        //File.AppendAllText(path2, content2);

        // Distance in docks
        //string path3 = Application.dataPath + "/DataAnalytics/log" + filenumber.ToString() + "DistanceLog" + ".txt";
        //GlobalScript.Filepath3 = path3;
        //File.WriteAllText(path3, "Start Distance log \n");
        //string content3 = "Login date and time: " + System.DateTime.Now + "\n\n";
        //File.AppendAllText(path3, content3);
    }
}
