using UnityEngine;

// Simple check for collisions
public class HitboxScript : MonoBehaviour
{
    public bool isColliding = false;

    void OnTriggerEnter(Collider collider)
    {
        isColliding = true;
    } 

    private void OnTriggerExit(Collider collider)
    {
        isColliding = false;
    }
}