using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;

public class GravityRegion : MonoBehaviour
{

    public bool gravityIsFlipped = false;
    public bool playerCanFlipGravity = true;

    public SpriteShapeRenderer background;

    private HashSet<GravityFlippable> flippables = new HashSet<GravityFlippable>();
    private Color gravityColor = new Color(0f, 0.12f, 0.34f, 1f);

    void Start()
    {
        BoxCollider2D collider = GetComponent<BoxCollider2D>();
        if (background != null)
        {
            Vector3 updatedScale = new Vector3(collider.size.x / 4f, collider.size.y / 4f, 1f);
            background.transform.localScale = updatedScale;
            background.transform.localPosition = new Vector3(collider.offset.x, collider.offset.y, 0f);
        }
    }

    void Update()
    {
        if (background != null)
        {
            background.color = gravityIsFlipped ? gravityColor : Color.clear;
        }
    }

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
