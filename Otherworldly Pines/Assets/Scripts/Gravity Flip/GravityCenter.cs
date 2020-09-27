using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravityCenter : MonoBehaviour
{

    [HideInInspector]
    public HashSet<GravityAffected> owners = new HashSet<GravityAffected>();
    private BoxCollider2D center;

    void Start()
    {
        center = gameObject.AddComponent<BoxCollider2D>() as BoxCollider2D;
        center.isTrigger = true;
        center.size = new Vector2(0.01f, 0.01f);
    }
    
}
