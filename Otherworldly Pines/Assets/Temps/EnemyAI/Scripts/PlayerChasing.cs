using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(EnemyBehavior))]
public class PlayerChasing : MonoBehaviour
{
    private EnemyBehavior behavior;
    private GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        this.behavior = gameObject.GetComponent<EnemyBehavior>();
        this.player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        if(this.behavior.isChasing()){
            if(this.player.transform.position.x - gameObject.transform.position.x < 0){
                this.behavior.moveLeft();
            }
            else{
                this.behavior.moveRight();
            }
        }
    }
}
