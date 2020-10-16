using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


// TODO: boxcast
[RequireComponent(typeof(EnemyBehavior))]
public class Vision : MonoBehaviour
{
    private EnemyBehavior behavior;
    private TargetLocking targetLocking;
    public float maxDistant = 7;

    private LayerMask playerMask;
    private LayerMask berriesMask;
    private LayerMask groundMask; 
    private LayerMask visionMask;

    // Start is called before the first frame update
    void Start()
    {
        this.behavior = gameObject.GetComponent<EnemyBehavior>();
        this.targetLocking = gameObject.GetComponent<TargetLocking>();

        playerMask = LayerMask.GetMask("Player");
        berriesMask = LayerMask.GetMask("Berries");
        groundMask = LayerMask.GetMask("Ground", "Pushables");

        // Combines the three masks
        visionMask = LayerMask.GetMask("Player","Berries","Ground","Pushables");
    }
    
    private bool IsInMask(GameObject obj, LayerMask mask) {
        return mask == (mask | (1 << obj.layer));
    }

    // Update is called once per frame
    void Update()
    { 
        if(!this.behavior.isEating()){
            float outsideBound = this.behavior.getDirection() * (this.behavior.getCollider().bounds.extents.x + 0.01f) + this.GetComponent<Collider2D>().bounds.center.x;
            
            RaycastHit2D hitTop = Physics2D.Raycast(
                new Vector2(
                    outsideBound,
                    this.behavior.getCollider().bounds.center.y
                ), 
                new Vector2(this.behavior.getDirection(),0),
                maxDistant,
                visionMask
            );

            RaycastHit2D hitBottom = Physics2D.Raycast(
                new Vector2(
                    outsideBound,
                    this.behavior.getCollider().bounds.center.y - this.behavior.getCollider().bounds.extents.y + 0.1f
                ), 
                new Vector2(this.behavior.getDirection(),0),
                maxDistant,
                visionMask
            );

            float distantTop = Mathf.Abs(hitTop.point.x - outsideBound);
            float distantBottom = Mathf.Abs(hitBottom.point.x - outsideBound);

            GameObject[] targets = {null, null}; 
            if(hitTop.collider != null && distantTop < maxDistant){
                targets[0] = hitTop.collider.gameObject;
            }
            if(hitBottom.collider != null && distantBottom < maxDistant){
                targets[1] = hitBottom.collider.gameObject;    
            }

            GameObject player = Array.Find(targets, target => target != null && IsInMask(target, playerMask));
            GameObject berry = Array.Find(targets, target => target != null && IsInMask(target, berriesMask));
            GameObject ground = Array.Find(targets, target => target != null && IsInMask(target, groundMask));
            
            if (player != null) {
                if (!behavior.isChasing()) {
                    this.targetLocking.setTarget(player);
                    this.behavior.chase();
                }
            } else if (berry != null) {
                if(!behavior.isInvestigating()){
                    Debug.Log("Investigate");
                    this.targetLocking.setTarget(berry);
                    this.behavior.investigate();
                }
            } else if (ground != null) {
                if (Mathf.Min(distantBottom, distantTop) < 0.2f){
                    if (this.behavior.getDirection() == 1) this.behavior.turnLeft();
                    else this.behavior.turnRight();

                    this.behavior.patrol();
                }
            } else {
                // if (!this.behavior.isPatrolling()) {
                //     Debug.Log("Patrol");
                //     this.behavior.reInitPatrol();
                //     this.behavior.patrol();
                // }
                
            }   
        }
    }
    
    // Vision: green
    private void OnDrawGizmosSelected() {
        if (this.behavior != null && this.behavior.getCollider() != null) {
            float outsideBound = this.behavior.getDirection() * (this.behavior.getCollider().bounds.extents.x + 0.01f) + this.GetComponent<Collider2D>().bounds.center.x;
            int layer_mask = LayerMask.GetMask("Player","Berries", "Ground");    
            RaycastHit2D hitTop = Physics2D.Raycast(
                new Vector2(
                    outsideBound,
                    this.behavior.getCollider().bounds.center.y
                ), 
                new Vector2(this.behavior.getDirection(),0),
                maxDistant,
                layer_mask
            );

            RaycastHit2D hitBottom = Physics2D.Raycast(
                new Vector2(
                    outsideBound,
                    this.behavior.getCollider().bounds.center.y - this.behavior.getCollider().bounds.extents.y + 0.1f
                ), 
                new Vector2(this.behavior.getDirection(),0),
                maxDistant,
                layer_mask
            );

            Gizmos.color = Color.green;
            Gizmos.DrawLine(new Vector2(
                    outsideBound ,
                    this.behavior.getCollider().bounds.center.y
                ), 
                hitTop.point);
            Gizmos.DrawLine(new Vector2(
                outsideBound,
                this.behavior.getCollider().bounds.center.y - this.behavior.getCollider().bounds.extents.y + 0.1f
            ),hitBottom.point);
            }

        
    }
}
