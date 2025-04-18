using UnityEngine;
using TMPro;

// Handles the player's movement
public class PlayerScript : MonoBehaviour
{
    public GameObject spawn;
    public GameObject berry;
    public GameObject carrot;
    public GameObject hitbox;
    public int lives;
    public float movementSpeed;
    public float berryMass;
    public float carrotMass;
    public float berryJumpSpeed;
    public float carrotJumpSpeed;
    public TextMeshProUGUI berriesBackgroundText;
    public TextMeshProUGUI berriesText;
    public TextMeshProUGUI carrotsBackgroundText;
    public TextMeshProUGUI carrotsText;
    public Animator berryAnimator;
    public Animator carrotAnimator;
    public AudioSource switchSound;
    public AudioSource failSound;

    private Rigidbody rb;
    private bool isGrounded;
    private bool isWalking;
    private bool canJump;
    private float jumpSpeed;
    private bool isBerry = false;
    private bool isCarrot = true;
    private int berriesCount = 0;
    private int carrotsCount = 0;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log("is grounded? " + isGrounded + ", can jump? " + canJump);

        // Moves the player left or right
        rb.linearVelocity = new Vector3(Input.GetAxis("Horizontal") * movementSpeed, rb.linearVelocity.y, rb.linearVelocity.z);

        // Rotates the player when changing direction
        if (Input.GetAxis("Horizontal") != 0)
        {
            transform.rotation = Quaternion.LookRotation(new Vector3(Input.GetAxis("Horizontal"), 0f, 0f));
            transform.Rotate(0f, 90f, 0f);

            // Checks if the player is walking on the ground or not
            if (isGrounded)
            {
                isWalking = true;
            }
            else
            {
                isWalking = false;
            }
        }
        // Otherwise face forward
        else
        {
            transform.rotation = Quaternion.LookRotation(new Vector3(-1f, 0f, 0f));
            isWalking = false;
        }

        // Allows the player to switch between characters if there is enough space
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            //!hitbox.GetComponent<HitboxScript>().isColliding
            if (true)
            {
                isBerry = !isBerry;
                isCarrot = !isCarrot;
                berry.SetActive(isBerry);
                carrot.SetActive(isCarrot);
                //switchSound.Play();
            }
            else
            {
                //failSound.Play();
            }
        }

        // Adjusts the player's variables and animations based on the character
        if (isBerry)
        {
            rb.mass = berryMass;
            jumpSpeed = berryJumpSpeed;
            berryAnimator.SetBool("isWalking", isWalking);
            berryAnimator.SetBool("isJumping", !isGrounded);
        }
        else if (isCarrot)
        {
            rb.mass = carrotMass;
            jumpSpeed = carrotJumpSpeed;
            carrotAnimator.SetBool("isWalking", isWalking);
            carrotAnimator.SetBool("isJumping", !isGrounded);
        }

        // Handles player jumping
        if (Input.GetButtonDown("Jump") && canJump)
        {
            rb.AddForce(transform.up * jumpSpeed, ForceMode.Impulse);
            canJump = false;
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
            berriesCount += 1;
            berriesBackgroundText.text = "BERRIES: " + berriesCount;
            berriesText.text = "BERRIES: " + berriesCount;
        }

        // Checks if the player is colliding with a carrot collectible
        if (collision.gameObject.tag == "Carrot" && isCarrot)
        {
            Destroy(collision.gameObject);
            carrotsCount += 1;
            carrotsBackgroundText.text = "CARROTS: " + carrotsCount;
            carrotsText.text = "CARROTS: " + carrotsCount;
        }
    }

    // Called if the player is colliding with something
    void OnCollisionStay(Collision collision)
    {
        // Checks if colliding with the top of the collision
        if (collision.contacts[0].normal.y == 1)
        {
            isGrounded = true;

            // Checks if the player is colliding with the ground
            if (collision.gameObject.tag == "Ground")
            {
                canJump = true;
            }
        }
    }

    // Called when the player leaves a collision
    void OnCollisionExit(Collision collision)
    {
        isGrounded = false;

        // Prevents the player from jumping in the air after falling off of a platform
        if (collision.gameObject.tag == "Ground")
        {
            canJump = false;
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