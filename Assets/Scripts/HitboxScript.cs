using UnityEngine;

// Simple check for collisions
public class HitboxScript : MonoBehaviour
{
    public bool isColliding = false;

    void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.tag != "Berry" && collision.gameObject.tag != "Carrot")
        {
            isColliding = true;
        }
    } 

    private void OnTriggerExit(Collider collision)
    {
        if (collision.gameObject.tag != "Berry" && collision.gameObject.tag != "Carrot")
        {
            isColliding = false;
        }
    }
}