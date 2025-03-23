using UnityEngine;

// Handles the player's movement
public class Player : MonoBehaviour
{
    public GameObject spawn;
    public float movementSpeed;
    public float jumpSpeed;
    public int lives;

    private Rigidbody rb;
    private bool isGrounded;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        // moves the player left or right
        rb.linearVelocity = new Vector3((Input.GetAxis("Horizontal")) * movementSpeed, rb.linearVelocity.y, rb.linearVelocity.z);

        //rotates the player when changing direction
        if (Input.GetAxis("Horizontal") != 0)
        {
            transform.rotation = Quaternion.LookRotation(new Vector3((Input.GetAxis("Horizontal")) * movementSpeed, 0f, 0f));
        }

        // handles player jumping
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            rb.AddForce(transform.up * jumpSpeed, ForceMode.Impulse);
            isGrounded = false;
        }

        // Respawns the player if they fall below the level
        if (transform.position.y < -10)
        {
            Respawn();
        }
    }

    // Called if the player collides with something
    void OnCollisionEnter(Collision collision)
    {
        // Checks if the player is colliding with the ground
        if (collision.gameObject.tag == "Ground")
        {
            if (collision.contacts[0].normal.y == 1) // Checks if colliding with the top of the collision
            {
                isGrounded = true;
            }
        }
    }

    // Moves the player to the spawn point and removes one of the player's lives
    public void Respawn()
    {
        transform.position = spawn.transform.position;
        transform.rotation = spawn.transform.rotation;
        rb.linearVelocity = new Vector3(0f, 0f, 0f);
        lives -= 1;
    }
}