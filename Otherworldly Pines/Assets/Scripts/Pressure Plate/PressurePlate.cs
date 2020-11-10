using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface PressurePlateActivated {

    void PPEnable();
    void PPDisable();
    void setColor(Color c);

}

public class PressurePlate : MonoBehaviour {
    
    public GameObject target;
    public LayerMask validContactsMask;
    private HashSet<int> validContants = new HashSet<int>();
    private Color myColor;

    private PressurePlateActivated activatable;

    private void Start() {
        activatable = target.GetComponentInChildren<PressurePlateActivated>();
        myColor = GetComponent<SpriteRenderer>().color;
        activatable.setColor(myColor); //default white, set instance color to change activatable's color
        activatable.PPDisable();
    }

    private void OnValidate() {
        if (target != null && target.GetComponentInChildren<PressurePlateActivated>() == null)
            Debug.LogError("Target must implement PressurePlateActivated", gameObject);
    }

    private void OnTriggerStay2D(Collider2D other) {
        if (((1 << other.gameObject.layer) & validContactsMask) != 0) {
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

    private void OnDrawGizmos() {
        Gizmos.color = Color.green;
        Gizmos.DrawLine(gameObject.transform.position, target.transform.position);
    }

}
