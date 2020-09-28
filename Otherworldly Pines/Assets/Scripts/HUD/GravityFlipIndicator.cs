using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GravityFlipIndicator : MonoBehaviour {

    public Text text;
    public GravityControl controller;

    public void GravityRegionChanged(GravityRegion activeGravityRegion) {
        if (activeGravityRegion == null || !activeGravityRegion.playerCanFlipGravity) {
            DisplayText("Disabled");
        } else if (activeGravityRegion.playerCanFlipGravity) {
            DisplayText("Enabled");
        }
    }

    private void DisplayText(string message) {
        text.text = "Gravity Flip: " + message;
    }

}
