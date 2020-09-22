using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravityControl : MonoBehaviour
{

    private GravityRegion activeGravityRegion;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.W) && activeGravityRegion != null)
        {
            activeGravityRegion.FlipGravity();
        }
    }

    public void EnterGravityRegion(GravityRegion region)
    {
        this.activeGravityRegion = region;
    }

    public void ExitGravityRegion(GravityRegion region)
    {
        if (activeGravityRegion.gameObject.GetInstanceID() == region.gameObject.GetInstanceID())
        {
            this.activeGravityRegion = null;
        }
    }
}
