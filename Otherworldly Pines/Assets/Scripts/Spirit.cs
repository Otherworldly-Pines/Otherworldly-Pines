using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spirit : MonoBehaviour
{
    public float speed;
    public GameObject player;
    private Rigidbody2D body;

    private Transform playerTransform;

    private Vector2 targetPosition;
    
    void Start() {
        body = GetComponent<Rigidbody2D>();
        playerTransform = player.GetComponent<Transform>();
    }

    private float Sigmoid(float z) {
        return 1f / (1f + Mathf.Exp(-z));
    }

    private float CurrentSpeed(float distanceToAnchor) {
        return 7f * Sigmoid(3f * (distanceToAnchor - 1.75f)) + 1f;
    }

    private Vector2 CurrentAnchorOffset(float t) {
        return new Vector2(1.5f * Mathf.Cos(t), 0.75f * Mathf.Sin(2f * t)) * 0.3f;
    }

    // Update is called once per frame
    void Update() {
        Vector3 anchorTarget = playerTransform.position - new Vector3(Mathf.Sign(playerTransform.localScale.x), -0.5f, -5f);
        anchorTarget = (Vector2) anchorTarget + CurrentAnchorOffset(Time.time * 2f);
        float currentDistance = Vector2.Distance(transform.position, anchorTarget);
        float currentSpeed = CurrentSpeed(currentDistance);

        body.velocity = (anchorTarget - transform.position).normalized * currentSpeed;
    }
}
