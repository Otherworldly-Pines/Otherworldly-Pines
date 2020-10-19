using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    private Scene currentScene;

    public static bool GameIsPaused = false;
    public static bool IsDisabledByTutorialText = false;

    public GameObject pausePanelUI;
    public GameObject pauseMenuUI;
    public GameObject optionMenuUI;
    public GameObject helpMenuUI;

    private void Awake() {
        IsDisabledByTutorialText = false;
    }

    void Start()
    {
        pausePanelUI.SetActive(false);
        pauseMenuUI.SetActive(false);
        optionMenuUI.SetActive(false);
        helpMenuUI.SetActive(false);
        GameIsPaused = false;
    }

    void Update()
    {
        if (IsDisabledByTutorialText) return;
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            if (GameIsPaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
    }

    public void Resume()
    {
        pausePanelUI.SetActive(false);
        pauseMenuUI.SetActive(false);
        optionMenuUI.SetActive(false);
        helpMenuUI.SetActive(false);
        Time.timeScale = 1f;
        GameIsPaused = false;
    }

    void Pause()
    {
        pausePanelUI.SetActive(true);
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        GameIsPaused = true;
    }

    public void LoadOptions()
    {
        optionMenuUI.SetActive(true);
    }

    public void BacktoPauseMenu()
    {
        optionMenuUI.SetActive(false);
        
    }

    public void Help()
    {
        helpMenuUI.SetActive(true);   
    }

    public void backtoOptionsMenu()
    {
        helpMenuUI.SetActive(false);
    }

    public void returnToCheckpoint()
    {
        currentScene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(currentScene.name);
        Resume();
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void exitLevel()
    {
        SceneManager.LoadScene(SceneIdentifier.MainMenu);
    }
}
