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

    private Vector3 originalPosition;
    private Vector3 destinationPosition;
    
    private Vector3 currentTarget;
    private Vector3 nextTarget;

    private bool isPaused;
    private float pauseTimer;

    void Start() {
        body = GetComponentInChildren<Rigidbody2D>();
        
        originalPosition = this.gameObject.transform.position;
        destinationPosition = destination.transform.position;

        currentTarget = destinationPosition;
        nextTarget = originalPosition;

        StartMoving();
    }

    private void SwapTargets() {
        Vector3 temp = currentTarget;
        currentTarget = nextTarget;
        nextTarget = temp;
    }

    private bool IsAtTarget() {
        return Vector3.Distance(transform.position, currentTarget) < 0.001f;
    }

    private void Pause() {
        pauseTimer = 0f;
        isPaused = true;
        body.velocity = Vector2.zero;
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

}
