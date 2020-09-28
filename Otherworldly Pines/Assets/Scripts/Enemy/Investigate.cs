using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Investigate : MonoBehaviour
{
    // Start is called before the first frame update
    private EnemyBehavior behavior;
    private TargetLocking targetLocking;
    public float speed = 3;
    // Start is called before the first frame update
    void Start()
    {
        this.behavior = gameObject.GetComponent<EnemyBehavior>();
        this.targetLocking = gameObject.GetComponent<TargetLocking>();
    }

    // Update is called once per frame
    void Update()
    {
        if(this.behavior.isInvestigating()){
            Vector2 targetDirection = new Vector2(this.targetLocking.getTarget().transform.position.x - gameObject.transform.position.x, 0).normalized;
            float movementRate = 1;
            if(this.behavior.isExausted()){
                movementRate = this.behavior.getExaustedMovementRate();        
            }
            gameObject.transform.Translate(targetDirection * Time.deltaTime * this.speed * movementRate, 0);
        }
    }
}
