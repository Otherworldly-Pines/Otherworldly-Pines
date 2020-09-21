using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Patrol))]
public class EnemyBehavior : MonoBehaviour
{
    private int direction = 0;
    public float speed = 1;
    private int state = 1; //0 is resting, 1 is moving, 
    private Patrol patrolController;
    // Start is called before the first frame update
    void Start()
    {
        this.patrolController = gameObject.GetComponent<Patrol>();
    }

    // Update is called once per frame
    void Update()
    {
        switch(this.state){
            case 1:
                moveWithSpeedAmplifier(1);
                break;
            case 2:
                moveWithSpeedAmplifier(2);
                break;
            default:
                break;
        }
       
    }

    void moveWithSpeedAmplifier(int amplifier){
        gameObject.transform.Translate(new Vector2(this.direction * Time.deltaTime * this.speed *amplifier, 0));
    }

    public void moveLeft(){
        this.direction = -1;
	}

    public void moveRight(){
        this.direction = 1;
	}

    public void rest(){
        this.state = 0;
	}

    public bool isResting(){
        return this.state == 0;
	}

    public bool isPatrolling(){
        return this.state == 1;
    }

    public bool isChasing(){
        return this.state == 2;
    }


    public void chase(){
        this.state = 2;
    }
    
    public void patrol(){
        this.state = 1;
    }

    public void reInitPatrol(){
        this.patrolController.initPatrolRange();
    }

    public IEnumerator restForSeconds(float second){
        int tmp = this.state;
        this.state = 0;
        yield return new WaitForSeconds(second);
        this.state = tmp;
	}
}
