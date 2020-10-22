using System;
using UnityEngine;

public class BerryBush : MonoBehaviour {

    public int numBerries = 3;

    private void OnTriggerEnter2D(Collider2D other) {
        if (numBerries <= 0) return;
        
        var throwControls = other.gameObject.GetComponent<PlayerThrow>();

        if (throwControls != null) {
            throwControls.Collect(numBerries);
            numBerries = 0;
            
            // TODO: just change sprite or something instead
            gameObject.SetActive(false);
        }
    }

}
