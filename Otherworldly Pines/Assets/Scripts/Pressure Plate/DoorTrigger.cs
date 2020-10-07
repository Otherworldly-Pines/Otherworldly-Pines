using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorTrigger : MonoBehaviour
{
    [SerializeField] private GameObject door;
    public LayerMask validContactsMask;
    private HashSet<int> validContants = new HashSet<int>();

    private void OnCollisionEnter2D(Collision2D other) {
        if (((1 << other.gameObject.layer) & validContactsMask) != 0) {
            validContants.Add(other.gameObject.GetInstanceID());
            door.gameObject.SetActive(false);
        }
    }

    private void OnCollisionExit2D(Collision2D other) {
        if (((1 << other.gameObject.layer) & validContactsMask) != 0) {
            validContants.Remove(other.gameObject.GetInstanceID());
            if (validContants.Count == 0) door.gameObject.SetActive(true);
        }
    }

}
