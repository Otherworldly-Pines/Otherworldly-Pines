using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Investigate behavior of the enemy
[RequireComponent(typeof(EnemyBehavior))]
public class Investigate : MonoBehaviour
{
    // Start is called before the first frame update
    private EnemyBehavior behavior; 
    private TargetLocking targetLocking;
    public float speed = 3; // Speed of movement

    // Start is called before the first frame update
    void Start()
    {
        this.behavior = gameObject.GetComponent<EnemyBehavior>();
        this.targetLocking = gameObject.GetComponent<TargetLocking>();
    }

    // Update is called once per frame
    // Move toward the target 
    void Update()
    {
        if(this.behavior.isInvestigating()){
            if(this.targetLocking.getTarget().transform.position.x >  gameObject.transform.position.x){
                this.behavior.turnRight();
            }
            else{
                this.behavior.turnLeft();
            }
            float movementRate = 1;
            if(this.behavior.isExausted()){
                movementRate = this.behavior.getExaustedMovementRate();        
            }

            gameObject.transform.Translate(new Vector2(this.behavior.direction, 0) * Time.deltaTime * this.speed * movementRate, 0);
        }
    }

    private void OnCollisionEnter2D(Collision2D other) {
        if(other.collider.tag == "Berries"){
            StartCoroutine(eatThenDestroy(other.collider.gameObject));
        }
    }

    IEnumerator eatThenDestroy(GameObject berry){
        yield return StartCoroutine(this.behavior.eatBerries());
        Destroy(berry);
    }
}
