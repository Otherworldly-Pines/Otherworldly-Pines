using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PushPullBlock : MonoBehaviour {

    private static float HARD_MASS = 200f;

    [HideInInspector] public bool shouldFreezePosition = false;
    [HideInInspector] public bool isBeingMoved = false;
    [HideInInspector] public BoxCollider2D collider;
    [SerializeField] private LayerMask playerMask;
    private Rigidbody2D body;
    private float border = 0.1f;
    private bool isPlayerNear = false;
    private GroundChecker groundChecker;
    public bool isHard = false;

    private SliderJoint2D joint;
    public PhysicsMaterial2D highFrictionMaterial;
    private PhysicsMaterial2D originalMaterial;
    private float originalMass;

    void Start() {
        collider = GetComponent<BoxCollider2D>();
        joint = GetComponent<SliderJoint2D>();
        groundChecker = GetComponent<GroundChecker>();
        body = GetComponent<Rigidbody2D>();
        originalMaterial = collider.sharedMaterial;
        
        originalMass = body.mass;
    }

    public bool IsGrounded() {
        return groundChecker.IsGrounded();
    }

    private void Update() {
        Vector2 size = collider.bounds.size + new Vector3(2f * border, 0f, 0f);
        RaycastHit2D hitInfo = Physics2D.BoxCast(collider.bounds.center, size, 0f, Vector2.down, 0f, playerMask);

        bool lastPlayerNear = isPlayerNear;
        isPlayerNear = hitInfo.collider != null;

        if (isPlayerNear && !lastPlayerNear) {
            Harden();
        } else if (!isPlayerNear && lastPlayerNear) {
            Soften();
        }
    }
    public bool IsPlayerNear(){
        return isPlayerNear;
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
        body.mass = originalMass;
        isHard = false;
    }

    public static void SoftenAll() {
        var all = FindObjectsOfType<PushPullBlock>();
        
        foreach (var block in all) {
            if (block.isHard) {
                block.Soften();
            }
        }
    }

    public void Harden() {
        collider.sharedMaterial = highFrictionMaterial;
        body.mass = HARD_MASS;
        isHard = true;
    }

    private void OnValidate() {
        collider = GetComponent<BoxCollider2D>();
    }

    private void OnDrawGizmosSelected() {
        if (!collider) collider = GetComponent<BoxCollider2D>();
        
        Gizmos.color = isHard ? Color.red : Color.yellow;
        GizmosUtility.DrawCollider(collider);
        
        Gizmos.color = Color.green;
        Vector2 size = collider.bounds.size + new Vector3(2f * border, 0f, 0f);
        GizmosUtility.DrawBox(transform.position, size);
    }

}
