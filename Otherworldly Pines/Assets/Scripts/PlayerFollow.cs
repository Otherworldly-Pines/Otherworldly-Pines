using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFollow : MonoBehaviour
{
    public float speed;
    public GameObject player;

    public float frequency;
    public float magnitude;
    public float stoppingDistX;
    public float stoppingDistY;

    private Transform playerTransform;
    private Rigidbody2D playerRigidBody;
    // Start is called before the first frame update
    void Start()
    {
        playerTransform = player.GetComponent<Transform>();
        playerRigidBody = player.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        //Moving
        if(playerRigidBody.velocity.magnitude > 0){
            Vector2 newPosition = new Vector2(playerTransform.position.x - stoppingDistX, playerTransform.position.y + stoppingDistY);

            transform.position = Vector2.MoveTowards(transform.position, newPosition, speed * Time.deltaTime);
        }
        //Idle
        else{
            Vector2 newPosition = new Vector2(playerTransform.position.x - stoppingDistX, playerTransform.position.y * Mathf.Sin(Time.time * frequency)*magnitude);

            transform.position = Vector2.MoveTowards(transform.position, newPosition, speed * Time.deltaTime);

        }
    }

}
