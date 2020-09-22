using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravityRegion : MonoBehaviour
{

    private int angle = 0;
	public bool gravityIsFlipped = false;
	public bool playerCanFlipGravity = true;
    private HashSet<GravityFlippable> flippables = new HashSet<GravityFlippable>();

	public void FlipGravity()
	{
		// Don't flip gravity if it's fixed
		if (!playerCanFlipGravity) return;

		gravityIsFlipped = !gravityIsFlipped;
		foreach (GravityFlippable flippable in flippables)
		{
			flippable.Flip();
		}
	}

    void FlipPlayer()
    {
        // angle = angle + 180;
        // player.transform.eulerAngles = new Vector3(0, 0, angle);
        // Vector3 Scaler = player.transform.localScale;
        // Scaler.x *= -1;
        // player.transform.localScale = Scaler;
        // rb.gravityScale *= -1;
        // if (player.GetComponent<PlayerMovement>().isUpsideDown)
        //     player.GetComponent<PlayerMovement>().isUpsideDown = false;
        // else
        //     player.GetComponent<PlayerMovement>().isUpsideDown = true;
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        GravityControl controller = collider.GetComponent<GravityControl>();
		if (controller != null) {
			controller.EnterGravityRegion(this);
		}
		
        GravityFlippable flippable = collider.GetComponent<GravityFlippable>();
        if (flippable != null) {
			flippables.Add(flippable);
        	if (this.gravityIsFlipped != flippable.isUpsideDown)
        	{
            	flippable.Flip();
        	}
		}
    }

    private void OnTriggerExit2D(Collider2D collider)
    {
        GravityControl controller = collider.GetComponent<GravityControl>();
		if (controller != null) {
			controller.ExitGravityRegion(this);
		}
		
        GravityFlippable flippable = collider.GetComponent<GravityFlippable>();
        if (flippable != null) {
			flippables.Remove(flippable);
		}
    }
}
