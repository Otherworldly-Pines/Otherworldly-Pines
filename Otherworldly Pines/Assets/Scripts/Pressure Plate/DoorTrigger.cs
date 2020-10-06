using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorTrigger : MonoBehaviour
{
    [SerializeField] GameObject door;

    bool hasTriggered = false;

    void OnTriggerEnter2D(Collider2D collider){

        if(!hasTriggered){
            door.transform.position = new Vector2(door.transform.position.x, door.transform.position.y - door.transform.localScale.y);
            hasTriggered = true;
        }
        

    }

}
