using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface PressurePlateActivated {

    void PPEnable();
    void PPDisable();

}

public class PressurePlate : MonoBehaviour {
    
    public GameObject target;
    public LayerMask validContactsMask;
    private HashSet<int> validContants = new HashSet<int>();

    private PressurePlateActivated activatable;

    private void Start() {
        activatable = target.GetComponentInChildren<PressurePlateActivated>();
        activatable.PPDisable();
    }

    private void OnValidate() {
        if (target != null && target.GetComponentInChildren<PressurePlateActivated>() == null)
            Debug.LogError(gameObject.name + "'s target must implement PressurePlateActivated");
    }

    private void OnTriggerStay2D(Collider2D other) {
        if (((1 << other.gameObject.layer) & validContactsMask) != 0) {
            Debug.Log(other.gameObject.name);
            validContants.Add(other.gameObject.GetInstanceID());
            activatable.PPEnable();
            gameObject.GetComponent<Renderer>().enabled = false;
        }
    }

    private void OnTriggerExit2D(Collider2D other) {
        if (((1 << other.gameObject.layer) & validContactsMask) != 0) {
            validContants.Remove(other.gameObject.GetInstanceID());
            if (validContants.Count == 0) {
                activatable.PPDisable();
                gameObject.GetComponent<Renderer>().enabled = true;
            }
        }
    }

    private void OnDrawGizmosSelected() {
        Gizmos.color = Color.green;
        Gizmos.DrawLine(gameObject.transform.position, target.transform.position);
    }

}
