using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PushPullBlock : MonoBehaviour {

    [HideInInspector] public bool shouldFreezePosition = false;
    [HideInInspector] public bool isBeingMoved = false;
    [HideInInspector] public BoxCollider2D collider;
    [SerializeField] private LayerMask playerMask;
    [SerializeField] private AudioClip boxThud;
    [SerializeField] private AudioClip boxMoving;
    private AudioSource boxSoundSource;
    private Rigidbody2D body;
    private float border = 0.5f;
    private bool isPlayerNear = false;
    private GroundChecker groundChecker;

    private SliderJoint2D joint;
    public PhysicsMaterial2D highFrictionMaterial;
    private PhysicsMaterial2D originalMaterial;
    private float originalMass;

    private bool isMoving = false;

    void Start() {
        boxSoundSource = GetComponent<AudioSource>();
        collider = GetComponent<BoxCollider2D>();
        joint = GetComponent<SliderJoint2D>();
        groundChecker = GetComponent<GroundChecker>();
        body = GetComponent<Rigidbody2D>();
        originalMaterial = collider.sharedMaterial;
        
        originalMass = body.mass;
        boxSoundSource.volume = GameSettings.sfxVolume;
    }

    public bool IsGrounded() {
        return groundChecker.IsGrounded();
    }

    void Update() {
        Vector2 size = collider.bounds.size + new Vector3(2f * border, 0f, 0f);
        RaycastHit2D hitInfo = Physics2D.BoxCast(collider.bounds.center, size, 0f, Vector2.down, 0f, playerMask);

        bool lastPlayerNear = isPlayerNear;
        isPlayerNear = hitInfo.collider != null;

        if (isPlayerNear && !lastPlayerNear) {
            Harden();
        } else if (!isPlayerNear && lastPlayerNear) {
            Soften();
        }

        boxSoundSource.volume = GameSettings.sfxVolume;

        //play sound effects
        isMoving = body.velocity != Vector2.zero;
        if (isMoving && isPlayerNear&& !boxSoundSource.isPlaying)
            boxSoundSource.PlayOneShot(boxMoving);
        if (!isMoving && boxSoundSource.isPlaying)
            boxSoundSource.Stop();
    }

    //play the thud noise when hitting the ground
    public void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.collider.tag == "Tilemap")
        {
            boxSoundSource.PlayOneShot(boxThud);
            Debug.Log("sound played");
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
    }

    public void Harden() {
        collider.sharedMaterial = highFrictionMaterial;
        body.mass = 100000000f;
    }

    private void OnValidate() {
        collider = GetComponent<BoxCollider2D>();
    }

    private void OnDrawGizmosSelected() {
        Gizmos.color = Color.yellow;
        Vector2 size = collider.bounds.size + new Vector3(2f * border, 0f, 0f);
        GizmosUtility.DrawBox(transform.position, size);
    }

}
