using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// TODO: Implement this enemy
public class AxolotAggro : BehaviorRelated {
    
    void Update() {
        if(this.behavior.isChasing()){
            if(this.behavior.isExausted()){
                // Do something 
            }
            else{
                // Do something
            }
        }
    }
}
