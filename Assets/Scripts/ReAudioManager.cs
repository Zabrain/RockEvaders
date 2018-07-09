using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ReAudioManager : MonoBehaviour {
    
    public AudioSource[] myAudioClipsTheme;

    public AudioSource[] myAudioClipsSFX;

    public static AudioSource[] myAudioClipsThemes = new AudioSource[2];
    public static AudioSource[] myAudioClipsSFXs = new AudioSource[9];

    public Slider myMusicSlider;
    public Slider mySFXSlider;

    public string stageName;

    // Use this for initialization
    void Start () {

        //Check if audio has been set before
        if (PlayerPrefs.GetInt("IsVolumeMusicSet") != 1)
        {
            PlayerPrefs.SetFloat("MusicVolume",1f);
            PlayerPrefs.SetInt("IsVolumeMusicSet", 1);
        }
        if (PlayerPrefs.GetInt("IsVolumeSFXSet") != 1)
        {
            PlayerPrefs.SetFloat("SFXVolume", 1f);
            PlayerPrefs.SetInt("IsVolumeSFXSet", 1);
        }

        if (stageName=="start")
        {
            StageNameStart();
        }
        if (stageName == "level1")
        {
            StageNameLevel1();
        }

        if(stageName == "SecondAnimation" || stageName == "FirstAnimation")
        {
            MovieSoundLogic();
        }


        //myAudioClipsTheme1[0] = myAudioClipsTheme1[0].GetComponent<AudioSource>();
        // myAudioClipsTheme1[0].enabled = true;

        //myAudioClipsTheme[1].Play();
    }

    //for start screen
    void StageNameStart()
    {
        //Instantiate Main theme music
        myAudioClipsThemes[0] = Instantiate(myAudioClipsTheme[0]);

        //Instantiate button click
        myAudioClipsSFXs[2] = Instantiate(myAudioClipsSFX[2]);

        //adjust the volumes of all the music and SFX if they exist(themes)
        SetVolumeOfThemesGeneral();
        SetVolumeOfSFXGeneral();

        myAudioClipsThemes[0].Play();//play theme music

        //Assign slider value
        if (myMusicSlider)
        {
            myMusicSlider.value = PlayerPrefs.GetFloat("MusicVolume");
        }
        if (mySFXSlider)
        {
            mySFXSlider.value = PlayerPrefs.GetFloat("SFXVolume");
        }
    }

    //for Stage 1
    void StageNameLevel1()
    {
        //Instantiate Main theme music
        for(int i = 0; i < myAudioClipsThemes.Length; i++)
        {
            myAudioClipsThemes[i] = Instantiate(myAudioClipsTheme[i]);
        }

        for (int i = 0; i < myAudioClipsSFXs.Length; i++)
        {
            myAudioClipsSFXs[i] = Instantiate(myAudioClipsSFX[i]);
        }

        //adjust the volumes of all the music and SFX if they exist(themes)
        SetVolumeOfThemesGeneral();
        SetVolumeOfSFXGeneral();

        myAudioClipsThemes[0].Play();//play theme music
                
    }

    //theme for movies
    public void MovieSoundLogic()
    {
        //Instantiate Main theme music
        myAudioClipsThemes[1] = Instantiate(myAudioClipsTheme[1]);

        //adjust the volumes of all the music and SFX if they exist(themes)
        SetVolumeOfThemesGeneral();
        SetVolumeOfSFXGeneral();

        if (myAudioClipsThemes[1])
        {
            myAudioClipsThemes[1].Play();
            myAudioClipsThemes[1].loop = true;
        }
    }

    //for slider music
    public void ChangeVolumeOfThemes()
    {
        PlayerPrefs.SetFloat("MusicVolume", myMusicSlider.value);

        SetVolumeOfThemesGeneral();
    }

    //for slider SFX
    public void ChangeVolumeOfSFX()
    {
        PlayerPrefs.SetFloat("SFXVolume", mySFXSlider.value);

        SetVolumeOfSFXGeneral();
    }

    //change all volumes of SFX
    public void SetVolumeOfSFXGeneral()
    {
        //adjust the volumes of all the music (themes)
        for (int i = 0; i < myAudioClipsSFXs.Length; i++)
        {
            //check if instantiated
            if (myAudioClipsSFXs[i])
            {
                myAudioClipsSFXs[i].volume = PlayerPrefs.GetFloat("SFXVolume");
            }
        }
    }
    //change all volumes of Themes
    public void SetVolumeOfThemesGeneral()
    {
        //adjust the volumes of all the music (themes)
        for (int i = 0; i < myAudioClipsThemes.Length; i++)
        {
            //check if instantiated
            if (myAudioClipsThemes[i])
            {
                myAudioClipsThemes[i].volume = PlayerPrefs.GetFloat("MusicVolume");
            }
        }
    }

    

    //Play Click
    public void ClickSound()
    {
        if (myAudioClipsSFXs[2])
        {
            myAudioClipsSFXs[2].Play();
        }
    }

    //Play Click
    public void BounceSound()
    {
        myAudioClipsSFXs[1] = Instantiate(myAudioClipsSFX[1]);
        if (myAudioClipsSFXs[1])
        {
            myAudioClipsSFXs[1].Play();
        }
    }

    //Play Click
    public void PauseSoundLogic()
    {
        if (myAudioClipsThemes[0] && myAudioClipsSFXs[2])
        {
            myAudioClipsThemes[0].Pause();
            myAudioClipsSFXs[2].Play();
        }
    }

    public void ResumeSoundLogic()
    {
        if (myAudioClipsThemes[0])
        {
            myAudioClipsThemes[0].Play();
            myAudioClipsThemes[0].loop=true;
        }
    }
   

    //AudioSource m_MyAudioSource;
    ////Value from the slider, and it converts to volume level
    //float m_MySliderValue;

    //void Start()
    //{
    //    //Initiate the Slider value to half way
    //    m_MySliderValue = 0.5f;
    //    //Fetch the AudioSource from the GameObject
    //    m_MyAudioSource = GetComponent<AudioSource>();
    //    //Play the AudioClip attached to the AudioSource on startup
    //    m_MyAudioSource.Play();
    //}

    //void OnGUI()
    //{
    //    //Create a horizontal Slider that controls volume levels. Its highest value is 1 and lowest is 0
    //    m_MySliderValue = GUI.HorizontalSlider(new Rect(25, 25, 200, 60), m_MySliderValue, 0.0F, 1.0F);
    //    //Makes the volume of the Audio match the Slider value
    //    m_MyAudioSource.volume = m_MySliderValue;
    //}

}
