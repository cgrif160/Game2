using UnityEngine;

public class ScrewScript : MonoBehaviour
{
    public GameObject top;
    public float speed;
    public float bottomLimit;


    // Called if the screw is colliding with something
    void OnCollisionStay(Collision collision)
    {
        // Checks if the player is colliding with the screw
        if (collision.gameObject.tag == "Player")
        {
            float movementInput = Input.GetAxis("Horizontal"); // Get the horizontal input (left/right arrow keys or A/D keys)

            if (movementInput < 0) // If the player is moving left
            {
                transform.position += Vector3.up * speed * Time.deltaTime; // Move the screw up
                top.transform.Rotate(new Vector3(0f, 180f, 0f) * Time.deltaTime);
                collision.gameObject.transform.position += Vector3.up * speed * Time.deltaTime;
            }
            else if (movementInput > 0 && transform.position.y > bottomLimit) // If the player is moving right
            {
                transform.position += Vector3.down * speed * Time.deltaTime; // Move the screw down
                top.transform.Rotate(new Vector3(0f, -180f, 0f) * Time.deltaTime);
                collision.gameObject.transform.position += Vector3.down * speed * Time.deltaTime;
            }
        }
    }
}