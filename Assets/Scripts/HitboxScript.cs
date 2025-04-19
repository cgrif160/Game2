using UnityEngine;

// Simple check for collisions
public class HitboxScript : MonoBehaviour
{
    public bool isColliding = false;

    void OnTriggerEnter(Collider collision)
    {
        isColliding = true;
    } 

    private void OnTriggerExit(Collider collision)
    {
        isColliding = false;
    }
}