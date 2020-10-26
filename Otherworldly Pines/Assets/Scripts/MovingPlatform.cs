using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using UnityEngine;

public class MovingPlatform : MonoBehaviour, PressurePlateActivated {

    public float movementSpeed = 2;
    public float pauseDuration = 0.5f;

    public LayerMask childables;

    public GameObject destination;
    private Rigidbody2D body;
    private BoxCollider2D collider;

    private Vector3 currentTarget;
    private Vector3 nextTarget;

    private bool isVertical;
    private bool isPaused;
    private float pauseTimer;

    private List<GameObject> children = new List<GameObject>();

    private bool isBlocked = false;

    private void Awake() {
        body = GetComponentInChildren<Rigidbody2D>();
        collider = GetComponent<BoxCollider2D>();

        currentTarget = destination.transform.position;
        nextTarget = gameObject.transform.position;

        isVertical = Mathf.Abs(destination.transform.localPosition.x) < 0.001f;
        
        StartMoving();
    }

    private void SwapTargets() {
        Vector3 temp = currentTarget;
        currentTarget = nextTarget;
        nextTarget = temp;
    }

    private bool IsAtTarget() {
        float actualDistance = Vector2.Distance(transform.position, nextTarget);
        float maxDistance = Vector2.Distance(currentTarget, nextTarget);
        return actualDistance > maxDistance - 0.001f;
    }

    private void Pause(bool snapToTarget = true) {
        pauseTimer = 0f;
        isPaused = true;
        body.velocity = Vector2.zero;
        if (snapToTarget) body.position = currentTarget;
        SwapTargets();
    }

    private Vector2 GetCurrentDirection() {
        return (currentTarget - transform.position).normalized;
    }

    private void StartMoving() {
        body.velocity = GetCurrentDirection() * movementSpeed;
    }

    void Update() {
        if (isPaused && !isBlocked) {
            pauseTimer += Time.deltaTime;

            if (pauseTimer > pauseDuration) {
                isPaused = false;
                if (isVertical) StartMoving();
            }
        }
      
        if (!isVertical && !isPaused && !isBlocked) {
            transform.position += (Vector3) (GetCurrentDirection() * Time.deltaTime * movementSpeed);
        }
    }

    private void FixedUpdate() {
        if (!isPaused && IsAtTarget()) Pause();
        if (!isVertical) UpdateChildren();
    }

    private bool IsInWay(Vector2 otherPosition) {
        if (isVertical) {
            return Math.Abs(Mathf.Sign(otherPosition.y - transform.position.y) - Mathf.Sign(GetCurrentDirection().y)) < 0.001f;
        } else {
            return Math.Abs(Mathf.Sign(otherPosition.x - transform.position.x) - Mathf.Sign(GetCurrentDirection().x)) < 0.001f;
        }
    }

    private void OnCollisionStay2D(Collision2D other) {
        LayerMask stoppables = LayerMask.GetMask("Pushables", "Player", "Enemies");
        if (other.gameObject && stoppables == (stoppables | (1 << other.gameObject.layer))) {
            // If the object is not in the way, don't worry about it
            if (!IsInWay(other.transform.position)) return;
            
            float distanceBetweenCenters = Vector3.Distance(collider.bounds.center, other.collider.bounds.center);
            Vector2 combinedColliderExtents = collider.bounds.extents + other.collider.bounds.extents;
            float maxAllowedDistance = isVertical ? combinedColliderExtents.y : combinedColliderExtents.x;
            if (distanceBetweenCenters < maxAllowedDistance) {
                Pause(false);
            }
        }
    }

    private void UpdateChildren() {
        ContactFilter2D filter = new ContactFilter2D();
        filter.SetLayerMask(childables);

        List<Collider2D> colliders = new List<Collider2D>();
        Physics2D.OverlapBox(transform.position, collider.size + new Vector2(0f, 0.1f), 0f, filter, colliders);

        List<GameObject> overlapped = colliders.Select(collider => collider.gameObject).ToList();
        
        // Add any new children
        foreach (var gameObj in overlapped) {
            if (!children.Contains(gameObj)) {
                gameObj.transform.SetParent(transform);
                children.Add(gameObj);
            }
        }

        // Remove any old children
        for (int i = 0; i < children.Count; i++) {
            if (!overlapped.Contains(children[i])) {
                children[i].transform.SetParent(null);
                children.RemoveAt(i);
                i--;
            }
        }
    }

    private List<GameObject> DoOverlapBox(Vector2 center, Vector2 size) {
        ContactFilter2D filter = new ContactFilter2D();
        filter.SetLayerMask(childables);

        List<Collider2D> colliders = new List<Collider2D>();
        Physics2D.OverlapBox(center, size, 0f, filter, colliders);
        return colliders.Select(collider => collider.gameObject).ToList();
    }

    public void PPEnable() {
        Debug.Log(gameObject.transform.parent.gameObject.name + " enabled");
        isBlocked = false;
        if (isVertical) StartMoving();
    }

    public void PPDisable() {
        Debug.Log(gameObject.transform.parent.gameObject.name + " disabled");
        isBlocked = true;
        if (isVertical) body.velocity = Vector2.zero;
    }

    private void OnValidate() {
        currentTarget = destination.transform.position;
        collider = GetComponent<BoxCollider2D>();
    }

    private void OnDrawGizmos() {
        Gizmos.color = Color.magenta;
        Gizmos.DrawRay(transform.position, currentTarget - transform.position);
        GizmosUtility.DrawBox(currentTarget, collider.bounds.size);
    }

    private void OnDrawGizmosSelected() {
        Gizmos.color = Color.yellow;
        GizmosUtility.DrawBox(transform.position, collider.bounds.size + new Vector3(0f, 0.1f, 0f));
    }

}