using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Thorn : MonoBehaviour
{ 
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //calls when the player hits the thorn; can be changed to other functions
    //now, it just assumes player dies instantly, and resets the scene
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.collider.tag == "Player")
        {
            collision.gameObject.GetComponent<PlayerHealth>().TakeDamage(34);
        }
    }
}
