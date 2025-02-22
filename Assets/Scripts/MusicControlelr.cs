using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MusicControlelr : MonoBehaviour
{
    private String sceneName;
    [SerializeField] private AudioClip mainMenuMusic;
    [SerializeField] private AudioClip levelMusic;
    [SerializeField] private AudioClip winMusic;
    [SerializeField] private AudioClip gameOverMusic;
    private AudioSource gameAudio;
    public bool end;



    public GameObject pauseController;

    private void Awake()
    {
        gameAudio = GetComponent<AudioSource>();

        Scene currentScene = SceneManager.GetActiveScene();
        sceneName = currentScene.name;

        PlayMusic();
    }


    private void PlayMusic()
    {
       // plays music depending on which scne we are 
            if (sceneName == "MainMenu")
            {
                gameAudio.clip = mainMenuMusic;
                gameAudio.loop = true;

                gameAudio.Play();

            }
            else if (sceneName == "WhiteBox")
            {
                gameAudio.clip = levelMusic;
                gameAudio.loop = true;

                gameAudio.Play();

            }
            else if (sceneName == "WIN")
            {

                gameAudio.clip = winMusic;
                gameAudio.loop = true;

                gameAudio.Play();


            }
        
    }
    public void GameOver()
    {
        gameAudio.clip = gameOverMusic;
        gameAudio.loop = true;
        gameAudio.Play();
    }

}

