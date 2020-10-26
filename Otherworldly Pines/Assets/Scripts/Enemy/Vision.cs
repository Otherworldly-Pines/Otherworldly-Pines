using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


// TODO: boxcast
public class Vision : BehaviorRelated
{
    public float maxDistant = 7;

    private LayerMask playerMask;
    private LayerMask berriesMask;
    private LayerMask groundMask; 
    private LayerMask visionMask;
    private LayerMask platesMask;

    // Start is called before the first frame update
    void Start()
    {
        playerMask = LayerMask.GetMask("Player");
        berriesMask = LayerMask.GetMask("Berries");
        groundMask = LayerMask.GetMask("Ground", "Pushables");
        platesMask = LayerMask.GetMask("Pressure Plates");

        // Combines the three masks
        visionMask = LayerMask.GetMask("Player", "Berries", "Ground", "Pushables", "Pressure Plates");
    }
    
    private bool IsInMask(GameObject obj, LayerMask mask) {
        return mask == (mask | (1 << obj.layer));
    }

    // Update is called once per frame
    void Update()
    { 
        if(!this.behavior.isEating()){
            float outsideBound = behavior.getDirection() * (collider.bounds.extents.x + 0.01f) + collider.bounds.center.x;
            
            RaycastHit2D hitTop = Physics2D.Raycast(
                new Vector2(outsideBound, collider.bounds.center.y), 
                GetDirectionVector(),
                maxDistant,
                visionMask
            );

            RaycastHit2D hitBottom = Physics2D.Raycast(
                new Vector2(outsideBound, collider.bounds.center.y - collider.bounds.extents.y + 0.1f), 
                GetDirectionVector(),
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
            GameObject plate = Array.Find(targets, target => target != null && IsInMask(target, platesMask));
            
            if (player != null) {
                if (!behavior.isChasing()) {
                    this.behavior.setTarget(player);
                    this.behavior.chase();
                }
            } else if (berry != null) {
                if(!behavior.isInvestigating()){
                    Debug.Log("Investigate");
                    this.behavior.setTarget(berry);
                    this.behavior.investigate();
                }
            } else if (plate != null && behavior.isPatrolling() && Mathf.Min(distantBottom, distantTop) < 0.1f) {
                if (this.behavior.getDirection() == 1) this.behavior.turnLeft();
                else this.behavior.turnRight();
            } else if (ground != null) {
                if (Mathf.Min(distantBottom, distantTop) < 0.2f){
                    if (this.behavior.getDirection() == 1) this.behavior.turnLeft();
                    else this.behavior.turnRight();

                    this.behavior.patrol();
                }
            }
        }
    }
    
    // Vision: green
    private void OnDrawGizmosSelected() {
        if (this.behavior != null && this.behavior.getCollider() != null) {
            float outsideBound = this.behavior.getDirection() * (collider.bounds.extents.x + 0.01f) + collider.bounds.center.x;
            int layer_mask = LayerMask.GetMask("Player","Berries", "Ground");    
            RaycastHit2D hitTop = Physics2D.Raycast(
                new Vector2(outsideBound, collider.bounds.center.y), 
                GetDirectionVector(),
                maxDistant,
                layer_mask
            );

            RaycastHit2D hitBottom = Physics2D.Raycast(
                new Vector2(
                    outsideBound,
                    collider.bounds.center.y - collider.bounds.extents.y + 0.1f
                ), 
                GetDirectionVector(),
                maxDistant,
                layer_mask
            );

            Gizmos.color = Color.green;
            Gizmos.DrawLine(new Vector2(outsideBound, collider.bounds.center.y), hitTop.point);
            Gizmos.DrawLine(new Vector2(outsideBound, collider.bounds.center.y - collider.bounds.extents.y + 0.1f), hitBottom.point);
        }
    }
}
