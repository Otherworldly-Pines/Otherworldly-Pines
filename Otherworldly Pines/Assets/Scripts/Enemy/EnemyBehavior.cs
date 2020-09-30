using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Patrol))]
public class EnemyBehavior : MonoBehaviour
{

    private int state = 1; //0 is resting, 1 is moving, 
    private Patrol patrolController; 
    private float stamina = 100; //Stamina for movement. Eneter resting state when reach 0
    private float maxStamina = 100; // Max stamina to end resting state
    private bool exausted = false; // Exausted state

    public float exaustRate = 1; // Amount of stamina use per second in neutral and investigate state
    public float regenRate = 2; // Amount of stamina regen per second in resting state
 
    public float exaustedMovementRate = 0.3f;  // Rate of moving when exausted
    public float aggroExaustRate = 2;  // Amount of stamina use per second in aggressive state.
    public int direction = 1; // Direction the enemy is moveing

    // Start is called before the first frame update 
    void Start()
    {
        this.patrolController = gameObject.GetComponent<Patrol>();
    }

    // Update is called once per frame
    // Update stamina every frame
    void Update()
    {
        updateStamina();
    }

    // Update stamina base on the state enemy is in
    void updateStamina(){
        if(!this.exausted){
            if(this.isChasing()){
                this.stamina -= Time.deltaTime * this.aggroExaustRate;
            }
            else{
                this.stamina -= Time.deltaTime * this.exaustRate;
            }

            if(this.stamina < 0){
                this.exausted = true;
            }
        }
        else{
            this.stamina += Time.deltaTime * this.regenRate;     
            if(this.stamina > this.maxStamina){
                this.exausted = false;
            }
        }
        
    }

    // Reinitialize patrol in patrol scripts
    public void reInitPatrol(){
        this.patrolController.initPatrolRange();
    }

    // Enter rest state for amount of seconds
    public IEnumerator restForSeconds(float second){
        int tmp = this.state;
        this.state = 0;
        yield return new WaitForSeconds(second);
        this.state = tmp;
	}

    // ------------ Getters and Setters------------------
    public void turnLeft(){
        this.direction = -1;
    }

    public void turnRight(){
        this.direction = 1;
    }

    public int getDirection(){
        return this.direction;
    }



    public void rest(){
        this.exausted = true;
	}


    public bool isExausted(){
        return this.exausted == true;
	}

    public bool isPatrolling(){
        return this.state == 1;
    }

    public bool isChasing(){
        return this.state == 2;
    }

    public bool isInvestigating(){
        return this.state == 3;
    }

    public void chase(){
        this.state = 2;
    }
    
    public void patrol(){
        this.state = 1;
    }
    
    public void investigate(){
        this.state = 3;
    }

    public float getExaustedMovementRate(){
        return this.exaustedMovementRate;
    }
}
