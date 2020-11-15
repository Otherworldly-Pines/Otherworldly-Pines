using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    private Scene currentScene;

    public static bool GameIsPaused = false;
    public static bool IsDisabledByTutorialText = false;

    public GameObject pausePanelUI;
    public GameObject pauseMenuUI;
    public GameObject optionMenuUI;
    public GameObject helpMenuUI;

    public Slider musicSlider;
    public Slider sfxSlider;

    private SoundManager soundManager;

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

        soundManager = FindObjectOfType<SoundManager>();
        musicSlider.value = GameSettings.musicVolume;
        sfxSlider.value = GameSettings.sfxVolume;
        Resume();
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
        pauseMenuUI.SetActive(false);
        optionMenuUI.SetActive(true);
    }

    public void BacktoPauseMenu()
    {
        optionMenuUI.SetActive(false);
        helpMenuUI.SetActive(false);
        pauseMenuUI.SetActive(true);
        
    }

    public void Help()
    {
        optionMenuUI.SetActive(false);
        helpMenuUI.SetActive(true);   
    }

    public void backtoOptionsMenu()
    {
        helpMenuUI.SetActive(false);
        optionMenuUI.SetActive(true);
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

    public void SetMusicVolume(float value) {
        GameSettings.musicVolume = value;
        soundManager.UpdateMusicVolume();
    }

    public void SetSfxVolume(float value) {
        GameSettings.sfxVolume = value;
        soundManager.UpdateSfxVolume();
    }
}
