using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


// TODO: Add check for another enemy
public class Vision : BehaviorRelated
{
    public float maxDistant = 7;

    public LayerMask playerMask;
    public LayerMask berriesMask;
    public LayerMask groundMask; 
    public LayerMask visionMask;
    public LayerMask platesMask;
    [SerializeField] private AudioClip eatingClip;
    [SerializeField] private AudioClip alertClip;
    private AudioSource deerSoundSource;

    // Start is called before the first frame update
    void Awake() {
        base.Awake();
        playerMask = LayerMask.GetMask("Player");
        berriesMask = LayerMask.GetMask("Berries");
        groundMask = LayerMask.GetMask("Ground", "Pushables");
        platesMask = LayerMask.GetMask("Pressure Plates");

        // Combines the three masks
        visionMask = LayerMask.GetMask("Player", "Berries", "Ground", "Pushables", "Pressure Plates");
        deerSoundSource = GetComponent<AudioSource>();
    }


    // Send out 10 raycasts instead of using BoxcastAll
    public List<RaycastHit2D> PerformRaycast() {
        var hits = new List<RaycastHit2D>();

        if (!behavior.isEating()) {
            var NUM_RAYS = 10f;
            var padding = 0.01f;
            var raycastX = collider.bounds.center.x + behavior.getDirection() * (collider.bounds.extents.x + padding);
            var height = collider.bounds.size.y - (2f * padding);
            var bottom = collider.bounds.center.y - (height / 2f);
            var inc = height / NUM_RAYS;

            for (var i = 0f; i <= NUM_RAYS; i++) {
                var hit = Physics2D.Raycast(
                    new Vector2(raycastX, bottom + (inc * i)),
                    GetDirectionVector(),
                    maxDistant,
                    visionMask
                );

                if (hit.collider) hits.Add(hit);
            }
        }

        return hits;
    }

    public static RaycastHit2D NearestHitObjectInLayer(IEnumerable<RaycastHit2D> hitlist, int layerMask) {
        foreach (var hit in hitlist) {
            if (MasksUtility.IsInMask(hit.collider.gameObject, layerMask)) {
                return hit;
            }
        }

        return new RaycastHit2D();
    }

    // Update is called once per frame
    void Update()
    {
        deerSoundSource.volume = GameSettings.sfxVolume;
        if (!behavior.isEating()) {
            var hits = PerformRaycast();

            var playerHit = NearestHitObjectInLayer(hits, playerMask);
            var berryHit = NearestHitObjectInLayer(hits, berriesMask);
            var groundHit = NearestHitObjectInLayer(hits, groundMask);
            var plateHit = NearestHitObjectInLayer(hits, platesMask);

            if (playerHit.collider != null) {
                if (!behavior.isChasing()) {
                    behavior.setTarget(playerHit.collider.gameObject);
                    behavior.chase();
                    deerSoundSource.PlayOneShot(alertClip);
                }
            } else if (!behavior.isChasing() && groundHit.collider != null && groundHit.distance < 0.1f) {
                behavior.flipDirection();
                behavior.patrol();
            } else if (berryHit.collider != null) {
                if (!behavior.isInvestigating()) {
                    behavior.setTarget(berryHit.collider.gameObject);
                    behavior.investigate();
                }
            } else if (plateHit.collider != null && behavior.isPatrolling() && plateHit.distance < 0.1f) {
                behavior.flipDirection();
            }
        }
        if (behavior.isEating() && !deerSoundSource.isPlaying)
        {
            deerSoundSource.PlayOneShot(eatingClip);
            Debug.Log("eating played");
        }
    }
    
    // Vision: green
    private void OnDrawGizmosSelected() {
        if (behavior != null && collider != null && !behavior.isEating()) {
            var NUM_RAYS = 10f;
            var padding = 0.01f;
            var raycastX = collider.bounds.center.x + behavior.getDirection() * (collider.bounds.extents.x + padding);
            var height = collider.bounds.size.y - (2f * padding);
            var bottom = collider.bounds.center.y - (height / 2f);
            var inc = height / NUM_RAYS;

            Gizmos.color = Color.green;
            for (var i = 0f; i <= NUM_RAYS; i++) {
                Gizmos.DrawRay(
                    new Vector2(
                        raycastX,
                        bottom + (inc * i)
                    ),
                    new Vector2(maxDistant * behavior.direction, 0f)
                );
            }
        }
    }
}
