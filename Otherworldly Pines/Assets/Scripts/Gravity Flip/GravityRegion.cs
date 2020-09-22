using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravityRegion : MonoBehaviour
{

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

    private void OnTriggerEnter2D(Collider2D collider)
    {
		// If the player just entered the region, inform them of that so they can control it
        GravityControl controller = collider.GetComponent<GravityControl>();
		if (controller != null) {
			controller.EnterGravityRegion(this);
		}
		
		// If a flippable object just entered, add them to this gravity region
		// and if they need their gravity updated, take care of that too
        GravityFlippable flippable = collider.GetComponent<GravityFlippable>();
        if (flippable != null) {
			flippables.Add(flippable);
        	if (gravityIsFlipped != flippable.isUpsideDown)
        	{
            	flippable.Flip();
        	}
		}
    }

    private void OnTriggerExit2D(Collider2D collider)
    {
		// If the player just exited, inform them of that
        GravityControl controller = collider.GetComponent<GravityControl>();
		if (controller != null) {
			controller.ExitGravityRegion(this);
		}
		
		// If a flippable object just exited, remove them from the list of flippables
        GravityFlippable flippable = collider.GetComponent<GravityFlippable>();
        if (flippable != null) {
			flippables.Remove(flippable);
		}
    }

}
