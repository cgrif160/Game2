using UnityEngine;

// Simple check for collisions
public class HitboxScript : MonoBehaviour
{
    public bool isColliding = false;

    void OnTriggerEnter(Collider collision)
    {
        isColliding = false;
    } 

    private void OnTriggerExit(Collider other)
    {
        isColliding = true;
    }
}