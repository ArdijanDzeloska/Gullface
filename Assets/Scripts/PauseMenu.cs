using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

public class PauseMenu : MonoBehaviour
{
    public bool isPaused;
    public GameObject pauseMenu;
    public InputField NameInputText;
    private string playerName;

    //tutorial
    public GameObject tutorials;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            isPaused = !isPaused;
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }

        if (isPaused)
        {
            ActivateMenu();
        }
        else
        {
            DeactivateMenu();
        }
    }

    void ActivateMenu()
    {
        Time.timeScale = 0;
        AudioListener.pause = true;
        pauseMenu.SetActive(true);
    }

    public void DeactivateMenu()
    {
        Time.timeScale = 1;
        AudioListener.pause = false;
        pauseMenu.SetActive(false);
        isPaused = false;
    }

    //void FixCursor()
    //{
    //    if (isPaused)
    //    {
    //        Cursor.visible = true;
    //    }
    //    else
    //    {
    //        Cursor.visible = false;
    //    }
    //}

    public void QuitGame()
    {
        SaveScore();
        //UnityEditor.EditorApplication.isPlaying = false;
        Application.Quit();
        // use this in final game
    }

    public void StartGame()
    {
        playerName = NameInputText.text;
        //Debug.Log(playerName);
        GlobalScript.Name = playerName;
        UnityEngine.SceneManagement.SceneManager.LoadScene("Docks");
        //Highscores.AddNewHighscore(playerName, 1000);
    }

    public void PlayAgain()
    {
        SaveScore();
        GlobalScript.Lives = 3;
        GlobalScript.Distance = 0;
        UnityEngine.SceneManagement.SceneManager.LoadScene("StartScreen");
    }

    public void SaveScore()
    {
        string leaderboard_path = Application.dataPath + "/DataAnalytics/Leaderboard.txt";
        string content;
        if (GlobalScript.Won)
        {
            content = GlobalScript.Name + ": won with " + GlobalScript.Lives.ToString() + " life/lives left.\n";
        }
        else
        {
            float dist = (float)System.Math.Round(GlobalScript.Distance, 0);
            content = GlobalScript.Name + ": lost with " + dist.ToString() + " meters travelled.\n";
        }

        File.AppendAllText(leaderboard_path, content);
    }
}
