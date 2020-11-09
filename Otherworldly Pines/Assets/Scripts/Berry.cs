using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Berry : MonoBehaviour {

    [HideInInspector] public bool isEdible = false;
    
    private Collider2D collider;

    private void Awake() {
        isEdible = false;
        collider = GetComponent<Collider2D>();
        collider.isTrigger = true;
    }

    private void OnTriggerExit2D(Collider2D other) {
        if (other.gameObject.CompareTag("Player")) {
            collider.isTrigger = false;
            isEdible = true;
        }
    }

}
