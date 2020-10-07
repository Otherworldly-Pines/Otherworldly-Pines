using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CrushDetection : MonoBehaviour
{

    private CapsuleCollider2D crushDetector;
    private MovingPlatform platformScript;
    private PlayerHealth healthScript;

    private bool imColliding;
    private GameObject collided;

    // Start is called before the first frame update
    void Start()
    {
        crushDetector = GetComponent<CapsuleCollider2D>();
        platformScript = GetComponent<MovingPlatform>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        imColliding = true;
        collided = collision.gameObject;
        if (collision.gameObject.tag == "Player")
        {
            //kill the player
            healthScript = collision.gameObject.GetComponent<PlayerHealth>();
            healthScript.TakeDamage(100.0f);
        }
        else if (collision.gameObject.tag == "Pushable")
        {
            platformScript.PushableEnter();
            //tell movingplatforms script to just pause the platform for pause duration then switch
        }
    }
}
