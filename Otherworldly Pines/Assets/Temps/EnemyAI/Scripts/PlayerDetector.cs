using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(EnemyBehavior))]
public class PlayerDetector : MonoBehaviour
{
    private EnemyBehavior behavior;
    private GameObject player;
    private float timeCounter = 0;
    public float detectionRange = 10;
    public float checkRate = 1;
    // Start is called before the first frame update
    void Start()
    {
        this.behavior = gameObject.GetComponent<EnemyBehavior>();
        this.player = GameObject.FindGameObjectWithTag("Player");
        
    }

    // Update is called once per frame
    void Update()
    {
        this.timeCounter += Time.deltaTime;
        if(this.timeCounter > this.checkRate){
            this.timeCounter = 0;
            checkPlayer();
        }
    }

    void checkPlayer(){
        if(seePlayer()){
            if(!this.behavior.isChasing())
            this.behavior.chase();
        }
        else{
            if(this.behavior.isChasing()){
                this.behavior.reInitPatrol();
                this.behavior.patrol();
            }   
        }
            
            
    }

    // Check if the player is visible to enemy
    bool seePlayer(){
        Vector2 toPlayer = new Vector2(
            this.player.transform.position.x - gameObject.transform.position.x, 
            this.player.transform.position.y - gameObject.transform.position.y
        );
        if(toPlayer.magnitude <= this.detectionRange){
            return true;
		}
        else{
            return false;
		}

        
	}
}
