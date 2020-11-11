using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadOnActivation : MonoBehaviour
{
    public string LevelName;
    void OnEnable()
    {
        SceneManager.LoadScene(LevelName, LoadSceneMode.Single);
    }
}
