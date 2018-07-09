using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class MoviesScript : MonoBehaviour {

    public bool introClipPlaying = false;

    public TextMeshProUGUI CurrentScore;

    public TextMeshProUGUI TotalVillagersSaved;

    public string MovieName;

	// Use this for initialization
	void Start () {
		
        if (MovieName == "SecondAnimation")
        {
            //Gets Current Score
            CurrentScore.text = "Current Score: " + PlayerPrefs.GetInt("Current_Score").ToString();

            //Get Total Villagers Acquired
            TotalVillagersSaved.text = "Bravo!!! "+"\n" + "You Saved "+ PlayerPrefs.GetInt("Villager_Total_Amount").ToString()+ " Villagers";
        }
        

    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void FirstGo()
    {
        SceneManager.LoadScene("RockEvaders");
        //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void ContinueGo()
    {
        SceneManager.LoadScene("RockEvaders");
        //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
