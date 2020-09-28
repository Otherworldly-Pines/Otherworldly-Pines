using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundChecker : MonoBehaviour {

    [Header("Component References")]
    public Collider2D collider;
    public Rigidbody2D body;
    public LayerMask groundLayerMask;

    [Header("Boxcast Properties")]
    public float percentColliderWidth = 1f;
    public float height = 0.01f;
    public Vector2 offset = Vector2.zero;
    private Vector2 size;

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
