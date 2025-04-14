using UnityEngine;

public class Screw : MonoBehaviour
{
    public GameObject player; // Reference to the player object

    public float moveSpeed = 1f; // Speed at which the screw moves

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // if the box collider on this object is colliding with the player's rigidbody, then detect their movement input. If its left, the screw should move up, if its right, the screw should move down.
        if(GetComponent<BoxCollider>().bounds.Intersects(player.GetComponent<BoxCollider>().bounds))
        {
            float input = Input.GetAxis("Horizontal"); // Get the horizontal input (left/right arrow keys or A/D keys)

            if (input < 0) // If the player is moving left
            {
                transform.position += Vector3.up * moveSpeed * Time.deltaTime; // Move the screw up
            }
            else if (input > 0) // If the player is moving right
            {
                transform.position += Vector3.down * moveSpeed * Time.deltaTime; // Move the screw down
            }
        }
    }
}
