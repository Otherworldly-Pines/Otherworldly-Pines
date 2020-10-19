using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingDamage : MonoBehaviour
{
    public float damageImpulseThreshold = 5000f;
    public float highestDamagingImpulse = 10000f;
    public float lowestDamageDealt = 10f;
    public float HighestDamageDealt = 30f;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        float collisionForce = GetImpactForce(collision);
        Debug.Log(collisionForce);
        if (collision.gameObject.tag == "Player")
        { 
            collision.gameObject.GetComponent<PlayerHealth>().TakeDamage(ProportionalDamageDeal(collisionForce));
        }
    }

    private float ProportionalDamageDeal(float impulse)
    {
        if (impulse < damageImpulseThreshold)
            return 0f;
        if (impulse < highestDamagingImpulse) // objectVelocity is big enough to deal damage
            return Mathf.Min((impulse / highestDamagingImpulse) * HighestDamageDealt + lowestDamageDealt, HighestDamageDealt);
        else
            return HighestDamageDealt;

    }

    private float GetImpactForce(Collision2D collision)
    {
        float impulse = 0F;

        foreach (ContactPoint2D point in collision.contacts)
        {
            impulse += point.normalImpulse;
        }

        return impulse / Time.fixedDeltaTime;
    }
}
