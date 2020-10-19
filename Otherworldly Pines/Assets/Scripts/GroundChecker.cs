using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Add this component to any object with a collider and rigidbody to
// give it the ability to check if it's on the ground or not
public class GroundChecker : MonoBehaviour {

    [HideInInspector] public Collider2D collider;
    [HideInInspector] public Rigidbody2D body;
    public LayerMask groundLayerMask;

    [Header("Boxcast Properties")]
    public float percentColliderWidth = 1f;
    public float height = 0.01f;
    public Vector2 offset = Vector2.zero;
    private Vector2 size;

    private void Start() {
        collider = GetComponent<Collider2D>();
        body = GetComponent<Rigidbody2D>();
    }

    public bool IsGrounded() {
        Physics2D.queriesStartInColliders = false;
        size = new Vector2(percentColliderWidth * collider.bounds.size.x, height);
        Vector2 verticalOffset = new Vector2(0f, collider.bounds.extents.y);
        Vector2 direction = (body != null && body.gravityScale < 0f) ? Vector2.up : Vector2.down;
        Vector2 origin = (Vector2) collider.bounds.center + (offset + verticalOffset) * direction;
        RaycastHit2D hitInfo = Physics2D.BoxCast(origin, size, 0f, direction, size.y, groundLayerMask);
        Physics2D.queriesStartInColliders = true;
        return hitInfo.distance > 0f;
    }

    private void OnDrawGizmosSelected() {
        if (!collider) collider = GetComponent<Collider2D>();
        Vector2 verticalOffset = new Vector2(0f, collider.bounds.extents.y);
        Vector2 direction = (body != null && body.gravityScale < 0f) ? Vector2.up : Vector2.down;
        Vector2 origin = (Vector2) collider.bounds.center + (offset + verticalOffset) * direction;
        
        Gizmos.color = Color.blue;
        Gizmos.DrawRay(origin - size / 2f, new Vector2(size.x, 0f));
        Gizmos.DrawRay(origin - size / 2f, new Vector2(0f, size.y));
        Gizmos.DrawRay(origin + size / 2f, new Vector2(-size.x, 0f));
        Gizmos.DrawRay(origin + size / 2f, new Vector2(0f, -size.y));
    }

}
