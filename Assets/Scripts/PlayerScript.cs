using UnityEngine;

// Handles the player's movement
public class PlayerScript : MonoBehaviour
{
    public GameObject spawn;
    public GameObject berry;
    public GameObject carrot;
    public int lives;
    public float movementSpeed;
    public float berryMass;
    public float carrotMass;
    public float berryJumpSpeed;
    public float carrotJumpSpeed;

    private Rigidbody rb;
    private bool isGrounded;
    private float jumpSpeed;
    private bool isBerry = false;
    private bool isCarrot = true;
    private int berryCount = 0;
    private int carrotCount = 0;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        // Moves the player left or right
        rb.linearVelocity = new Vector3(Input.GetAxis("Horizontal") * movementSpeed, rb.linearVelocity.y, rb.linearVelocity.z);

        // Rotates the player when changing direction
        if (Input.GetAxis("Horizontal") != 0)
        {
            transform.rotation = Quaternion.LookRotation(new Vector3(Input.GetAxis("Horizontal"), 0f, 0f));
            transform.Rotate(0f, 90f, 0f);
        }
        // Otherwise face forward
        else
        {
            transform.rotation = Quaternion.LookRotation(new Vector3(-1f, 0f, 0f));
        }

        // Allows the player to switch between characters
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            isBerry = !isBerry;
            isCarrot = !isCarrot;
            berry.SetActive(isBerry);
            carrot.SetActive(isCarrot);
        }

        // Adjusts the player's variables based on the character
        if (isBerry)
        {
            rb.mass = berryMass;
            jumpSpeed = berryJumpSpeed;
        }
        else if (isCarrot)
        {
            rb.mass = carrotMass;
            jumpSpeed = carrotJumpSpeed;
        }

        // Handles player jumping
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

    //  Called if the player collides with a trigger
    void OnTriggerEnter(Collider collision)
    {
        // Checks if the player is colliding with a berry collectible
        if (collision.gameObject.tag == "Berry" && isBerry)
        {
            Destroy(collision.gameObject);
            berryCount += 1;
        }

        // Checks if the player is colliding with a carrot collectible
        if (collision.gameObject.tag == "Carrot" && isCarrot)
        {
            Destroy(collision.gameObject);
            carrotCount += 1;
        }
    }

    // Called if the player is colliding with something
    void OnCollisionStay(Collision collision)
    {
        // Checks if the player is colliding with the ground
        if (collision.gameObject.tag == "Ground")
        {
            // Checks if colliding with the top of the collision
            if (collision.contacts[0].normal.y == 1)
            {
                isGrounded = true;
            }
        }
    }

    // Called when the player leaves a collision
    void OnCollisionExit(Collision collision)
    {
        // Prevents the player from jumping in the air after falling off of a platform
        if (collision.gameObject.tag == "Ground")
        {
            isGrounded = false;
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