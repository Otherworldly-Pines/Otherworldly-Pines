using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(EnemyBehavior))]
public class BehaviorRelated : MonoBehaviour {

    protected EnemyBehavior behavior;
    protected Rigidbody2D body;
    protected BoxCollider2D collider;

    private void Awake() {
        behavior = GetComponent<EnemyBehavior>();
        body = GetComponent<Rigidbody2D>();
        collider = GetComponent<BoxCollider2D>();
    }

    protected Vector2 GetDirectionVector() {
        return new Vector2(behavior.direction, 0f);
    }

    protected void MoveForwardBy(float dist) {
        gameObject.transform.Translate(GetDirectionVector() * dist);
    }
    
}
