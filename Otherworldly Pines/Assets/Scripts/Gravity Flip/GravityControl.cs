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
        
    }

    public void EnterGravityRegion(GravityRegion region)
    {
        this.activeGravityRegion = region;
    }

    public void ExitGravityRegion(GravityRegion region)
    {
        if (activeGravityRegion.gameObject.GetInstanceId() == region.gameObject.GetInstanceId())
        {
            this.activeGravityRegion = null;
        }
    }
}
