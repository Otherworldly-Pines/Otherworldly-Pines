using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CheckpointMaster : MonoBehaviour
{
    
    private static CheckpointMaster instance;
    
    [SerializeField] private int berryCount = 5;
    [SerializeField] public bool skipTutorial = true;
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
        }
        else
        {
            Destroy(gameObject);
        }

        GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerThrow>().setAmmo(berryCount);
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

        dialogBox.SetActive(!skipTutorial);
        currentScene = SceneManager.GetActiveScene();
        if (GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerHealth>().currentHealth <= 0)
        {
            SceneManager.LoadScene(currentScene.name);
        };
    }

    private void OnValidate() {
        if (startingPosition == null) Debug.LogError("Must assign a starting position", gameObject);
    }

}
