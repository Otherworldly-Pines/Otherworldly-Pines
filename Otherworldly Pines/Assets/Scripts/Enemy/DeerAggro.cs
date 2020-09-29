using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(EnemyBehavior))]
[RequireComponent(typeof(TargetLocking))]
public class DeerAggro : MonoBehaviour
{
    private float aggroSpeed = 5;
    private EnemyBehavior behavior;
    private TargetLocking targetLocking;
    
    private SpriteController spriteController;
    // Start is called before the first frame update
    void Start()
    {
        this.behavior = gameObject.GetComponent<EnemyBehavior>();
        this.targetLocking = gameObject.GetComponent<TargetLocking>();
        this.spriteController = gameObject.GetComponent<SpriteController>();
    }


    

    // Update is called once per frame
    void Update()
    {
        if(this.behavior.isChasing()){
            Vector2 direction;
            if(this.targetLocking.getTarget().transform.position.x > gameObject.transform.position.x){
                direction = new Vector2(1,0);
            }
            else{
                direction = new Vector2(-1, 0);
            }
            switch(direction.x){
                case 1:
                    this.spriteController.turnRight();
                    break;
                case -1:
                    this.spriteController.turnLeft();
                    break;
            }
            
            float movementRate = 1;
            if(this.behavior.isExausted()){
                movementRate = this.behavior.getExaustedMovementRate();        
            }
            gameObject.transform.Translate(direction * Time.deltaTime * this.aggroSpeed * movementRate, 0);
        }

    }
}
