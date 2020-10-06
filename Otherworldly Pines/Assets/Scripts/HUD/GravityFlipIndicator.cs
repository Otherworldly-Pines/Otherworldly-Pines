using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GravityFlipIndicator : MonoBehaviour {

    public Image image;

    public void GravityRegionChanged(GravityRegion activeGravityRegion) {
        if (activeGravityRegion == null || !activeGravityRegion.playerCanFlipGravity) {
            image.color = new Color(0.75f, 0.75f, 0.75f, 0.5f);
        } else if (activeGravityRegion.playerCanFlipGravity) {
            image.color = Color.white;
        }
    }

}
