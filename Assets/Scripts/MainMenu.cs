using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour {

    //loads the game scene
    public void PlayGame()
    {
        if (PlayerPrefs.GetInt("Level_Amount") < 1)
        {
            SceneManager.LoadScene("FirstAnimation");
        }
        else
        {
                SceneManager.LoadScene("SecondAnimation");
        }
    }

   
}
