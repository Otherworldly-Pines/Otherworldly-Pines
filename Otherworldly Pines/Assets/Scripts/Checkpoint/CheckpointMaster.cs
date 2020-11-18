using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CheckpointMaster : MonoBehaviour {
    
    public static Vector2 spawnPoint;
    public static int spawnAmmo;
    public static bool shouldResetToStartingPos = true;
    
    [SerializeField] private Transform startingPosition;
    private Scene currentScene;

    void Awake() {
        if (shouldResetToStartingPos | spawnPoint == null) {
            spawnPoint = startingPosition.position;
        }
    }

    private void OnValidate() {
        if (startingPosition == null) Debug.LogError("Must assign a starting position", gameObject);
    }

}
