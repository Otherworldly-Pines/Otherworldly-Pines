using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CheckpointMaster : MonoBehaviour
{
    
    private static CheckpointMaster instance;
    
    [SerializeField] public int berryCount = 5;
    [SerializeField] public bool skipTutorial = false;
    [SerializeField] private GameObject dialogBox;


    
    [SerializeField] private Transform startingPosition;
    [HideInInspector] public Vector2 lastCheckPointPos;
    private Scene currentScene;

    void Awake()
    {
        dialogBox = GameObject.FindGameObjectWithTag("DialogBox");

        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(instance);
            instance.lastCheckPointPos = startingPosition.position;
            GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerThrow>().setAmmo(berryCount);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        currentScene = SceneManager.GetActiveScene();

    }

    // Update is called once per frame
    void Update()
    {
        dialogBox = GameObject.FindGameObjectWithTag("DialogBox");

        if (dialogBox != null) dialogBox.SetActive(!skipTutorial);
        currentScene = SceneManager.GetActiveScene();

        var player = GameObject.FindGameObjectWithTag("Player");
        if (player == null) return;
        
        if (player.GetComponent<PlayerHealth>().currentHealth <= 0)
        {
            SceneManager.LoadScene(currentScene.name);
        };
    }

    private void OnValidate() {
        if (startingPosition == null) Debug.LogError("Must assign a starting position", gameObject);
    }

}
