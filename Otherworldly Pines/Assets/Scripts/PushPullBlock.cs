using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PushPullBlock : MonoBehaviour {

    [HideInInspector] public bool shouldFreezePosition = false;
    [HideInInspector] public bool isBeingMoved = false;
    [HideInInspector] public BoxCollider2D collider;
    [SerializeField] private LayerMask playerMask;
    private float border = 0.5f;
    private bool isPlayerNear = false;
    private GroundChecker groundChecker;

    private SliderJoint2D joint;
    public PhysicsMaterial2D highFrictionMaterial;
    private PhysicsMaterial2D originalMaterial;

    void Start() {
        collider = GetComponent<BoxCollider2D>();
        joint = GetComponent<SliderJoint2D>();
        groundChecker = GetComponent<GroundChecker>();
        originalMaterial = collider.sharedMaterial;
    }

    public bool IsGrounded() {
        return groundChecker.IsGrounded();
    }

    private void Update() {
        Vector2 size = collider.bounds.size + new Vector3(2f * border, -0.01f, 0f);
        RaycastHit2D hitInfo = Physics2D.BoxCast(collider.bounds.center, size, 0f, Vector2.down, 0f, playerMask);

        bool lastPlayerNear = isPlayerNear;
        isPlayerNear = hitInfo.collider != null;

        if (isPlayerNear && !lastPlayerNear) {
            Harden();
        } else if (!isPlayerNear && lastPlayerNear) {
            Soften();
        }
    }

    public void ConnectToBody(Rigidbody2D otherBody) {
        Soften();
        joint.connectedBody = otherBody;
        joint.enabled = true;
    }

    public void DisconnectFromBody() {
        joint.connectedBody = null;
        joint.enabled = false;
    }
    
    public void Soften() {
        collider.sharedMaterial = originalMaterial;
    }

    public void Harden() {
        collider.sharedMaterial = highFrictionMaterial;
    }

    private void OnDrawGizmosSelected() {
        Gizmos.color = Color.yellow;
        Vector2 size = collider.bounds.size + new Vector3(2f * border, -0.01f, 0f);
        Gizmos.DrawRay((Vector2)collider.bounds.center - size / 2f, new Vector2(size.x, 0f));
        Gizmos.DrawRay((Vector2)collider.bounds.center - size / 2f, new Vector2(0f, size.y));
        Gizmos.DrawRay((Vector2)collider.bounds.center + size / 2f, new Vector2(-size.x, 0f));
        Gizmos.DrawRay((Vector2)collider.bounds.center + size / 2f, new Vector2(0f, -size.y));
    }

}
