using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{

    private void Start() {
        MenuMusic.StartIfStopped();
    }

    public void PlayGame()
    {
        MenuMusic.StopPlaying();
        SceneManager.LoadScene("Intro Scene");
    }

    public void SkipToLevel2() {
        MenuMusic.StopPlaying();
        SceneManager.LoadScene(SceneIdentifier.Level2);
    }

    public void Instruction()
    {
        SceneManager.LoadScene(SceneIdentifier.Instructions);
    }

    public void Options()
    {
        SceneManager.LoadScene(SceneIdentifier.Options);
    }

    public void Credits()
    {
        SceneManager.LoadScene(SceneIdentifier.Credits);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

}
