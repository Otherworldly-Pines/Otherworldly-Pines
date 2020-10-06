using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IHUDConnected {

    void ConnectToHUD(HUD hud);

}

public class HUD : MonoBehaviour {

    public HealthBar healthBar;
    public GravityFlipIndicator gravityFlipIndicator;
    public StasisDisplay stasisDisplay;
    public BerryCounter berryCounter;

    private void Start() {
        GameObject player = GameObject.Find("Player");
        if (player == null) return;

        IHUDConnected[] connectables = player.GetComponentsInChildren<IHUDConnected>();
        foreach (IHUDConnected connectable in connectables) {
            connectable.ConnectToHUD(this);
        }
    }

}
