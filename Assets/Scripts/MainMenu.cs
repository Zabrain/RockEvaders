using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour {

    

    public void PlayNewGame()
    {
        SceneManager.LoadScene("FirstAnimation");

        //clear all player progress
        PlayerPrefs.SetInt("Current_Score", 0);
        PlayerPrefs.SetInt("Villager_Total_Amount", 0);
        PlayerPrefs.SetInt("Level_Amount", 0);
    }

    public void PlayContinue()
    {
        SceneManager.LoadScene("SecondAnimation");
    }

}
