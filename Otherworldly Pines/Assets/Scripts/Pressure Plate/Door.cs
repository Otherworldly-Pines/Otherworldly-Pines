using System;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;

[SelectionBase]
public class Door : MonoBehaviour, PressurePlateActivated {
    
    [SerializeField] private GameObject slidingPart;
    [SerializeField] private Vector2 slideDirection;
    private BoxCollider2D collider;
    private SpriteMask mask;
    private SpriteRenderer slidingRenderer;
    private float openSpeed = 2f;
    private Vector3 destination;
    private AudioSource doorSound;

    private static Color c_white = Color.white;
    private bool pressurePlateIsEnabled;

    private void Awake() {
        collider = GetComponent<BoxCollider2D>();
        slidingRenderer = slidingPart.GetComponent<SpriteRenderer>();
        mask = GetComponentInChildren<SpriteMask>();
        destination = Vector3.zero;
        doorSound = GetComponent<AudioSource>();
    }

    private void Update() {
        float distance = (destination - slidingPart.transform.localPosition).magnitude;
        float travelDistance = Mathf.Min(openSpeed * Time.deltaTime, distance);
        Vector3 diff = (destination - slidingPart.transform.localPosition).normalized * travelDistance;
        slidingPart.transform.localPosition += diff;
        
        UpdateCollider();
    }

    private void UpdateCollider() {
        var nextBounds = Intersection(mask.bounds, slidingRenderer.bounds);

        if (nextBounds.size.x < 0.001f || nextBounds.size.y < 0.001f) {
            collider.enabled = false;
        } else if (!collider.enabled) {
            collider.enabled = true;
        }

        var offset = nextBounds.center - transform.position;

        collider.offset = offset;
        collider.size = nextBounds.size;
    }

    private Bounds Intersection(Bounds b1, Bounds b2) {
        if (!b1.Intersects(b2)) return new Bounds(Vector3.zero, Vector3.zero);
        
        var min = Vector3.Max(b1.min, b2.min);
        var max = Vector3.Min(b1.max, b2.max);
        var center = (min + max) / 2;
        var size = max - min;

        return new Bounds(center, size);
    }

    public void PPEnable() {
        if (pressurePlateIsEnabled) return;
        pressurePlateIsEnabled = true;
        destination = Vector3.Scale(slidingRenderer.bounds.size, new Vector3(slideDirection.x, slideDirection.y, 1f));
        doorSound.Play();
    }

    public void PPDisable() {
        if (!pressurePlateIsEnabled) return;
        pressurePlateIsEnabled = false;
        destination = Vector3.zero;
        doorSound.Stop();
    }

    public void setColor(Color c) {
        GetComponent<SpriteRenderer>().color = c;
        slidingRenderer.color = c;
    }

}