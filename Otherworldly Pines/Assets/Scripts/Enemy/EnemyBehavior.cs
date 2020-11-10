using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[SelectionBase]
[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(BoxCollider2D))]
public class EnemyBehavior : MonoBehaviour {

    enum State : int {
        Eating = 0,
        Patrolling = 1,
        Investigating = 2,
        Chasing = 3,
    }

    private State state = State.Patrolling;
    private float stamina = 100; //Stamina for movement. Eneter resting state when reach 0
    private float maxStamina = 100; // Max stamina to end resting state
    private bool exausted = false; // Exausted state

    public Animator animator;

    private Rigidbody2D rigidbody;
    private BoxCollider2D collider;
    private GroundChecker groundChecker;

    private bool grounded = true;
    private LayerMask groundMask;
    
    public float exaustRate = 1; // Amount of stamina use per second in neutral and investigate state
    public float regenRate = 2; // Amount of stamina regen per second in resting state
 
    public float exaustedMovementRate = 0.3f;  // Rate of moving when exausted
    public float aggroExaustRate = 2;  // Amount of stamina use per second in aggressive state.
    public int direction = 1; // Direction the enemy is moveing
    public float eatTime = 3f;
    public GameObject sprite;

    private GameObject target;


    // Start is called before the first frame update 
    void Start()
    {
        this.animator = sprite.GetComponent<Animator>();
        this.collider = gameObject.GetComponent<BoxCollider2D>();
        this.rigidbody = gameObject.GetComponent<Rigidbody2D>();
        this.groundChecker = GetComponent<GroundChecker>();

        groundMask = LayerMask.GetMask("Ground", "Pushables");
        
        target = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    // Update stamina every frame
    void Update()
    {
        updateStamina();
        updateGrounded();
        // Debug.Log(this.state);
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

    public bool isGrounded(){
        return this.grounded;
    }

    void updateGrounded() {
        grounded = groundChecker.IsGrounded();
    }

    // ------------ Getters and Setters------------------
    
    public void setTarget(GameObject target){
        this.target = target;
    }

    public GameObject getTarget(){
        return target;
    }
    
    public void turnLeft(){
        this.direction = -1;
    }

    public void flipDirection(){
        this.direction *= -1;
    }

    public Rigidbody2D getRigidbody(){
        return this.rigidbody;
    }

    public BoxCollider2D getCollider(){
        return this.collider;
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
        return this.state == State.Patrolling;
    }

    public bool isChasing(){
        return this.state == State.Chasing;
    }

    public bool isInvestigating(){
        return this.state == State.Investigating;
    }

    public bool isEating(){
        return this.state == State.Eating;
    }

    public void eat(){
        this.state = State.Eating;
        this.animator.SetInteger("State", 0);
    }

    public void chase(){
        this.state = State.Chasing;
        this.animator.SetInteger("State", 2);
    }
    
    public void patrol(){
        this.state = State.Patrolling;
        this.animator.SetInteger("State", 1);
    }
    
    public void investigate(){
        this.state = State.Investigating;
        this.animator.SetInteger("State", 1);
    }

    public float GetCurrentMovementSpeed() {
        return exausted ? exaustedMovementRate : 1f;
    }

    public IEnumerator eatBerries(){
        this.eat();
        yield return new WaitForSeconds(this.eatTime);
        this.patrol();
    }
}
