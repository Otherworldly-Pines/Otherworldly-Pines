using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerThrow : MonoBehaviour, IHUDConnected {

    private BerryCounter berryCounter;
    public GameObject bulletPrefab;
    public float bulletSpeed = 10.0f;
    public float bulletDestroyTime = 3.0f;
    public int ammoPerBerry = 3;
    private int ammo = 20;
    
    private PlayerFreeze playerFreeze;

    private void Start() {
        playerFreeze = GetComponent<PlayerFreeze>();
        if (berryCounter) berryCounter.SetCount(ammo);
    }

    void Update() {
        if (!playerFreeze.IsHoveringAny() && Input.GetMouseButtonDown(0) && !PauseMenu.GameIsPaused) Shoot();
    }

    //let arm follow the mouse and flip
    void FollowMouse(GameObject input) {
        Vector3 difference = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        difference.Normalize();
        float z = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
        input.transform.rotation = Quaternion.Euler(0f, 0f, z); //arm follows mouse if face to the right
        //myPlayer.transform.rotation = Quaternion.Euler(0, 0, 0); //player faces right
        //Debug.Log(z);
        if(z < -90 || z > 90)
        {
            //if (myPlayer.transform.eulerAngles.y == 0 || myPlayer.transform.eulerAngles.y == 180)
            //{
                //input.transform.rotation = Quaternion.Euler(180, 0, -z); //arm follows mouse if face to the left
                //myPlayer.transform.rotation = Quaternion.Euler(0, 180, 0); //player faces left
            //}
        }
    }

    //fire the bullets
    void Shoot()
    {
        if (ammo > 0)
        {
            GameObject bullet = Instantiate(bulletPrefab) as GameObject;
            bullet.transform.position = transform.position;
            FollowMouse(bullet);

            Vector3 difference =
                Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y,
                    transform.position.z)) - transform.position;
            float distance = difference.magnitude;
            Vector2 direction = difference / distance;
            direction.Normalize();

            bullet.transform.position +=
                new Vector3(direction.x, direction.y, 0f); // bullets dont collide with player's collider
            bullet.GetComponent<Rigidbody2D>().velocity = direction * bulletSpeed;

            Destroy(bullet, bulletDestroyTime);

            ammo--;
        }
        
        berryCounter.SetCount(ammo);
    }
    
    // pick up ammo and destroy berries
    void OnCollisionEnter2D(Collision2D target)
    {
        if (target.gameObject.CompareTag("Berries"))
        {
            ammo += ammoPerBerry;
            Debug.Log("1");
            Destroy(target.gameObject);
        }
    }

    public void ConnectToHUD(HUD hud) {
        berryCounter = hud.berryCounter;
    }

}


