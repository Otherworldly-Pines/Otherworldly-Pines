using System;
using UnityEngine;

public class Eating : BehaviorRelated {

    protected void Update() {
        if (behavior.isEating()) {
            if (behavior.currentBerry == null) {
                behavior.patrol();
                return;
            }

            behavior.currentBerry.hp -= (100f / behavior.eatTime) * Time.deltaTime;
        }
    }

    private void OnTriggerEnter2D(Collider2D other) {
        var berry = other.gameObject.GetComponentInParent<Berry>();
        if (berry != null && behavior.IsTarget(berry.gameObject)) {
            behavior.eat(berry);
        }
    }

    private void OnTriggerExit2D(Collider2D other) {
        var berry = other.gameObject.GetComponentInParent<Berry>();
        if (berry != null && behavior.currentBerry != null && berry.GetInstanceID() == behavior.currentBerry.GetInstanceID()) {
            behavior.patrol();
        }
    }

}