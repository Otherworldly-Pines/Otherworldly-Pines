using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Patrol))]
public class EnemyBehavior : MonoBehaviour
{

    private int state = 1; //0 is resting, 1 is moving, 
    private Patrol patrolController;
    private float stamina = 100;
    private float maxStamina = 100;
    private bool exausted = false;

    public float exaustRate = 1;
    public float regenRate = 2;
 
    public float exaustedMovementRate = 0.3f; 
    public float aggroExaustRate = 2; 
    // Start is called before the first frame update 
    void Start()
    {
        this.patrolController = gameObject.GetComponent<Patrol>();
    }

    // Update is called once per frame
    void Update()
    {
        // switch(this.state){
        //     case 1:
        //         moveWithSpeedAmplifier(1);
        //         break;
        //     case 2:
        //         moveWithSpeedAmplifier(2);
        //         break;
        //     default:
        //         break;
        // }
        updateStamina();
    }

    // void moveWithSpeedAmplifier(int amplifier){
    //     if(this.exausted){
    //         gameObject.transform.Translate(new Vector2(this.direction * Time.deltaTime * this.speed * this.slowAmplifier, 0));
    //     }
    //     else{
    //         gameObject.transform.Translate(new Vector2(this.direction * Time.deltaTime * this.speed * amplifier, 0));
    //     }
    //     this.updateStamina(amplifier);
    // }

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

    public void reInitPatrol(){
        this.patrolController.initPatrolRange();
    }

    public float getExaustedMovementRate(){
        return this.exaustedMovementRate;
    }

    public IEnumerator restForSeconds(float second){
        int tmp = this.state;
        this.state = 0;
        yield return new WaitForSeconds(second);
        this.state = tmp;
	}
}
