using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour {

    public static bool gamePaused = false;

    public GameObject pauseMenu;

    public GameObject pauseButton;

    //The Lose Menu object
    public GameObject LoseMenu;

    // Update is called once per frame
    void Update () {
		
       

	}

    public void PauseGame()
    {
        gamePaused = true;
        Time.timeScale = 0f;
    }

    public void ResumeGame()
    {
        gamePaused = false;
        Time.timeScale = 1f;
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void MainMenu()
    {
        //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex-1);
        SceneManager.LoadScene("StartScreen");

        //MAKE TIME RESUME AFTER GOING BACK TO MAIN MENU
        Time.timeScale = 1f;
    }

    public void RestartGame()
    {
        SceneManager.LoadScene("RockEvaders");

        //MAKE TIME RESUME AFTER GOING BACK TO MAIN MENU
        Time.timeScale = 1f;
    }
}
