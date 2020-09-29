using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * Intended to be subclassed by any objects that will be interacting with a gravity field.
 * This class adds a really small box collider to the center of an object for detecting when
 * the object is in a field
 */
public class GravityAffected : MonoBehaviour
{

    [HideInInspector]
    public GravityCenter center;
    
    protected void Start()
    {
        // Check if this object already has an existing gravity center
        Transform possibleExistingCenter = transform.Find("Gravity Center");
        if (possibleExistingCenter != null)
        {
            GravityCenter possibleCenter = possibleExistingCenter.gameObject.GetComponent<GravityCenter>();
            if (possibleCenter != null)
            {
                // If it does, just use that one
                center = possibleCenter;
                center.owners.Add(this);
                return;
            }
        }
        
        // Otherwise, create a new one
        GameObject centerObject = new GameObject("Gravity Center", typeof(GravityCenter));
        centerObject.transform.parent = this.transform;
        centerObject.transform.localPosition = Vector3.zero;

        center = centerObject.GetComponent<GravityCenter>();
        center.owners.Add(this);
    }
    
}
