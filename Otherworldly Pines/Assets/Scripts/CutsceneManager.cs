using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CutsceneManager : MonoBehaviour {

    public bool skippable = false;

    private void Start() {
        var playerObject = GameObject.FindWithTag("Player");
        var controls = playerObject.GetComponent<PlayerControls>();
        var throwing = playerObject.GetComponent<PlayerThrow>();
        
        controls.FreezeControls();
        throwing.FreezeControls();
    }

    private void Update() {
        if (skippable && Input.GetKeyDown(KeyCode.Escape)) {
            SceneManager.LoadScene(SceneIdentifier.Level1);
        } 
    }

}