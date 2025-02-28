using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MusicControlelr : MonoBehaviour
{
    private String sceneName;
    [SerializeField] private AudioClip mainMenuMusic;
    [SerializeField] private AudioClip levelMusic;
    [SerializeField] private AudioClip winMusic;
    [SerializeField] private AudioClip gameOverMusic;
    [SerializeField] private AudioClip chaeMusic;


    [SerializeField] private AudioSource gameAudio;
    [SerializeField] private AudioSource chaseAudio;

    public bool end;

    private void Awake()
    {
        // Initialize AudioSources if not set in the Inspector
        if (gameAudio == null || chaseAudio == null)
        {
            AudioSource[] audios = GetComponents<AudioSource>();
            if (audios.Length >= 2)
            {
                gameAudio = audios[0];
                chaseAudio = audios[1];
            }
            else
            {
                Debug.LogError("Not enough AudioSources attached to the GameObject.");
            }
        }

        Scene currentScene = SceneManager.GetActiveScene();
        sceneName = currentScene.name;

        PlayMusic();
    }

    private void PlayMusic()
    {
        chaseAudio.clip = chaeMusic;
        chaseAudio.loop = true;
        chaseAudio.Play();
        chaseAudio.Pause();

        if (sceneName == "MainMenu")
        {
            gameAudio.clip = mainMenuMusic;
            gameAudio.loop = true;
            gameAudio.Play();
        }
        else if (sceneName == "WhiteBox" || sceneName == "WhiteBoxCopy")
        {
            PlayLevelMusic();
        }
    }

    public void GameOver()
    {
        chaseAudio.Pause();
        gameAudio.clip = gameOverMusic;
        gameAudio.loop = true;
        gameAudio.Play();
    }

    public void PWinMusic()
    {
        gameAudio.clip = winMusic;
        gameAudio.loop = true;
        gameAudio.Play();
    }

    public void PChaseMusic()
    {
        gameAudio.Pause();
        chaseAudio.UnPause();
    }

    public void ResumeMusic()
    {
        chaseAudio.Pause();
        gameAudio.UnPause();
    }

    public void PlayLevelMusic()
    {
        gameAudio.clip = levelMusic;
        gameAudio.loop = true;
        gameAudio.Play();
    }
}

