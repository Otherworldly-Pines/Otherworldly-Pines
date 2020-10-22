using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Thorn : MonoBehaviour
{
    public float knockDur = 0.01f;

    public float knockbackPower = 10f;


    //calls when the player hits the thorn; can be changed to other functions
    //now, it just assumes player dies instantly, and resets the scene
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.collider.tag == "Player")
        {
            collision.gameObject.GetComponent<PlayerHealth>().TakeDamage(34);
            PlayerControls player = collision.gameObject.GetComponent<PlayerControls>();
            player.Knockback(knockDur,knockbackPower,player.transform.localPosition);
        }
    }
}
