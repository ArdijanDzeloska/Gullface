using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;
using System.IO;

public class MenuStartScr : MonoBehaviour
{
    public bool isPaused;
    public GameObject pauseMenu;
    public InputField NameInputText;
    private string playerName;

    //tutorial
    public GameObject tutorials;
    public GameObject generalTutorial;
    public GameObject docksTutorial;
    public GameObject caveTutorial;

    //intro vid
    public GameObject introVideo;
    public GameObject introVideoPlayer;
    public GameObject skipButton;
    private float videoEndTime;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }

    void Update()
    {
        //if (introVideo.active)
        //{
        //    if (Time.time > videoEndTime)
        //    {
        //        skipButton.GetComponentInChildren<Text>().text = "Start";
        //    }
        //}
    }

    public void StartGame()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("Docks");
        //Highscores.AddNewHighscore(playerName, 1000);
    }

    public void ShowIntro()
    {
        playerName = NameInputText.text;
        //Debug.Log(playerName);
        GlobalScript.Name = playerName;
        introVideo.SetActive(true);
        float videoLength = (float)introVideoPlayer.GetComponent<VideoPlayer>().clip.length;
        //Debug.Log(videoLength);
        videoEndTime = (float)Time.time + videoLength;
        //Debug.Log(videoEndTime);
    }

    public void QuitGame()
    {
        UnityEditor.EditorApplication.isPlaying = false;
        //Application.Quit();
        // use this in final game
    }

    public void ShowTutorial()
    {
        tutorials.SetActive(true);
        pauseMenu.SetActive(false);
        generalTutorial.SetActive(true);
        docksTutorial.SetActive(false);
        caveTutorial.SetActive(false);
    }

    public void ShowNextTutorial()
    {
        if (generalTutorial.activeInHierarchy)
        {
            ShowDocksTutorial();
        }
        else if (docksTutorial.activeInHierarchy)
        {
            ShowCaveTutorial();
        }
        else if (caveTutorial.activeInHierarchy)
        {
            QuitTutorial();
        }
    }

    public void ShowDocksTutorial()
            {
        docksTutorial.SetActive(true);
        generalTutorial.SetActive(false);
    }

    public void ShowCaveTutorial()
    {
        caveTutorial.SetActive(true);
        docksTutorial.SetActive(false);
    }

    public void QuitTutorial()
    {
        pauseMenu.SetActive(true);
        tutorials.SetActive(false);
    }
}
