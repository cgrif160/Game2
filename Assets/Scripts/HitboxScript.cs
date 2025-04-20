using UnityEngine;

// Simple check for collisions
public class HitboxScript : MonoBehaviour
{
    public Transform target;
    public bool isColliding = false;

    // Update is called once per frame
    void Update()
    {
        // Only follows the target if it exists
        if (target != null)
        {
            // Moves the hitbox with the target
            transform.position = new Vector3(target.position.x, target.position.y, target.position.z);
        }
    }

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