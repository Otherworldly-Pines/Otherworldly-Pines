using UnityEngine;

public class GizmosUtility {

    public static void DrawCollider(Collider2D colliderToShow) {
        DrawBox(colliderToShow.bounds.center, colliderToShow.bounds.size);
    }

    public static void DrawBounds(Bounds bounds) {
        DrawBox(bounds.center, bounds.size);
    }

    public static void DrawBox(Vector2 center, Vector2 size) {
        Gizmos.DrawRay(center - size / 2f, new Vector2(size.x, 0f));
        Gizmos.DrawRay(center - size / 2f, new Vector2(0f, size.y));
        Gizmos.DrawRay(center + size / 2f, new Vector2(-size.x, 0f));
        Gizmos.DrawRay(center + size / 2f, new Vector2(0f, -size.y));
    }

}