using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void PlayGame()
    {
        SceneManager.LoadScene(SceneIdentifier.Level1);
    }

    public void SkipToLevel2() {
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
