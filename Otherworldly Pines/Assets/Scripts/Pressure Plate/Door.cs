using System;
using UnityEngine;
using UnityEngine.SceneManagement;

[SelectionBase]
public class Door : MonoBehaviour, PressurePlateActivated {
    [SerializeField] private GameObject slidingPart;
    private BoxCollider2D collider;
    private SpriteRenderer slidingRenderer;
    private float openSpeed = 2f;
    private Vector3 destination;
    private AudioSource doorSound;

    private void Awake() {
        collider = GetComponent<BoxCollider2D>();
        slidingRenderer = slidingPart.GetComponent<SpriteRenderer>();
        destination = transform.position;
        doorSound = GetComponent<AudioSource>();
    }

    private void Update() {
        float distance = (destination - slidingPart.transform.position).magnitude;
        float travelDistance = Mathf.Min(openSpeed * Time.deltaTime, distance);
        Vector3 diff = (destination - slidingPart.transform.position).normalized * travelDistance;
        slidingPart.transform.position += diff;
    }

    public void PPEnable() {
        collider.enabled = false;
        destination = transform.position - new Vector3(0f, slidingRenderer.bounds.size.y, 0f);
        doorSound.Play();
    }

    public void PPDisable() {
        collider.enabled = true;
        destination = transform.position;
        doorSound.Stop();
    }

}