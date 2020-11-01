using UnityEngine;

public class BasicAggro : BehaviorRelated {

    public float forgetDistance = 7;
    public float aggroSpeed = 5;

    protected void Update() {
        if (behavior.isChasing() && behavior.getTarget() != null) {
            var distanceToTarget = Vector2.Distance(transform.position, behavior.getTarget().transform.position);
            if (distanceToTarget > forgetDistance) {
                behavior.setTarget(null);
                behavior.investigate();
            }
        }

        if(this.behavior.isGrounded() && this.behavior.isChasing()){
            if(this.behavior.getTarget().transform.position.x > gameObject.transform.position.x){
                this.behavior.turnRight();
            }
            else{
                this.behavior.turnLeft();
            }

            float movementRate = behavior.GetCurrentMovementSpeed();
            MoveForwardBy(Time.deltaTime * aggroSpeed * movementRate);
        }
    }

}