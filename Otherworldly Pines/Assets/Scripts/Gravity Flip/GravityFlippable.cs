using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravityFlippable : MonoBehaviour
{

    private Rigidbody2D body;
    public bool isUpsideDown = false;

    // Start is called before the first frame update
    void Start()
    {
        body = GetComponentInParent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    // Flip gravity
    public void Flip()
    {
        body.gravityScale *= -1;
        isUpsideDown = body.gravityScale < 0;
		
		Vector3 localScale = transform.localScale;
		localScale.y *= -1;
		transform.localScale = localScale;
    }
}
