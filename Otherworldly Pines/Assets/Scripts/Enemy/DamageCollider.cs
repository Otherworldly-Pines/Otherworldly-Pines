using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageCollider : MonoBehaviour
{
    public float damage = 10f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D other) {  
        if(other.collider.tag == "Player"){
            PlayerHealth playerHealth = other.gameObject.GetComponent<PlayerHealth>();
            playerHealth.TakeDamage(this.damage);
        }
    }

    private void OnCollisionStay2D(Collision2D other) {
        // if(other.collider.tag == "Player"){
        //     // Debug.Log("Stay");
        // }
    }
}
