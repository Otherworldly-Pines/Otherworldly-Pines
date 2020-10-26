using UnityEngine;

public class MasksUtility {

    public static bool IsInMask(GameObject obj, LayerMask mask) {
        return mask == (mask | (1 << obj.layer));
    }

}