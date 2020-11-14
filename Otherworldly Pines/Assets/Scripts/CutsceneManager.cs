using System;
using UnityEngine;

public class CutsceneManager : MonoBehaviour {

    private void Start() {
        var playerObject = GameObject.FindWithTag("Player");
        var controls = playerObject.GetComponent<PlayerControls>();
        var throwing = playerObject.GetComponent<PlayerThrow>();
        
        controls.FreezeControls();
        throwing.FreezeControls();
    }

}