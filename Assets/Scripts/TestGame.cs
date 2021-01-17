using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Net;

public class TestGame : MonoBehaviour
{
    int scoring = 0;

    void Start()
    {

      //  scoring = 1000;
        scoring += (int)GlobalScript.Distance * 10;
        scoring -= (int)Time.time * 3;

        //if (GameFlow.player.SlowDown)
      //  {
       //     scoring -= 100;
       // }
       
        if (GlobalScript.Won)
        {
            scoring += GlobalScript.Lives * 3000;
            
        }
        if (scoring < 0){
            scoring = 0;
        }
        
        Highscores.AddNewHighscore(GlobalScript.Name, scoring);
     }
}

