using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFollow : MonoBehaviour
{
    public float speed;
    public GameObject player;

    private Transform playerTransform;
    // Start is called before the first frame update
    void Start()
    {
        playerTransform = player.GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        //set where the spirit should be with regards to player
        Vector2 newPosition = new Vector2(playerTransform.position.x - 2f, playerTransform.position.y + 3f);
        //follow the player 
        transform.position = Vector2.MoveTowards(transform.position, newPosition, speed * Time.deltaTime);
    }
}
