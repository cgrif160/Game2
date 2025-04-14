using UnityEngine;

// Simple check for collisions
public class HitboxScript : MonoBehaviour
{
    public bool isColliding;

    void OnTriggerStay(Collider collision)
    {
        isColliding = false;
    } 

    private void OnTriggerExit(Collider other)
    {
        isColliding = true;
    }
}