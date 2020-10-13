using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    public float movementSpeed = 2;
    public float pauseDuration = 0.5f;

    public GameObject destination;
    private Rigidbody2D body;
    
    private Vector3 currentTarget;
    private Vector3 nextTarget;

    private bool isPaused;
    private float pauseTimer;

    void Start() {
        body = GetComponentInChildren<Rigidbody2D>();

        currentTarget = destination.transform.position;
        nextTarget = gameObject.transform.position;

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
        return actualDistance > maxDistance;
    }

    private void Pause() {
        pauseTimer = 0f;
        isPaused = true;
        body.velocity = Vector2.zero;
        body.position = currentTarget;
        SwapTargets();
    }

    private void StartMoving() {
        Vector2 direction = (currentTarget - transform.position).normalized;
        body.velocity = direction * movementSpeed;
    }

    void Update() {
        if (isPaused) {
            pauseTimer += Time.deltaTime;

            if (pauseTimer > pauseDuration) {
                isPaused = false;
                StartMoving();
            }
        }
    }

    private void FixedUpdate() {
        if (!isPaused && IsAtTarget()) Pause();
    }

    private void OnDrawGizmos() {
        Gizmos.color = Color.magenta;
        Gizmos.DrawRay(transform.position, destination.transform.position - transform.position);

        Collider2D collider = GetComponent<Collider2D>();
        GizmosUtility.DrawBox(destination.transform.position, collider.bounds.size);
    }

}
