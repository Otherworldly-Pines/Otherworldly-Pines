using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class KillBox : MonoBehaviour
{
    private Scene currentScene;

    private void Start()
    {
        currentScene = SceneManager.GetActiveScene();

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        
        if(collision.collider.tag == "Player")
        {
            collision.gameObject.GetComponent<PlayerHealth>().TakeDamage(100000000);
            SceneManager.LoadScene(currentScene.name);
        }
    }
}
