using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    private static bool cutscenePlayed = false;

    private void Start() {
        MenuMusic.StartIfStopped();
        Time.timeScale = 1f;
        PauseMenu.GameIsPaused = false;
    }

    public void PlayGame() {
        CheckpointMaster.shouldResetToStartingPos = true;
        MenuMusic.StopPlaying();
        if (!cutscenePlayed)
        {
            SceneManager.LoadScene("Intro Scene");
            cutscenePlayed = true;
        }
        else SceneManager.LoadScene(SceneIdentifier.Level1);
    }

    public void SkipToLevel2() {
        CheckpointMaster.shouldResetToStartingPos = true;
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
