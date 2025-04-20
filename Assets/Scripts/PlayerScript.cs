using UnityEngine;
using System.Collections.Generic;
using TMPro;

// Handles the player's movement and interactions
public class PlayerScript : MonoBehaviour
{
    public List<Transform> checkpoints = new List<Transform>();
    public GameObject berry;
    public GameObject carrot;
    public GameObject hitbox;
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
    public AudioSource switchFailSound; 

    private Rigidbody rb;
    private float movementInput;
    private bool isGrounded;
    private bool isWalking;
    private bool isJumping;
    private bool canJump;
    private float jumpSpeed;
    private bool isBerry = false;
    private bool isCarrot = true;
    private int berriesCount = 0;
    private int carrotsCount = 0;
    private Transform currentCheckpoint;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        currentCheckpoint = checkpoints[0];
    }

    // Update is called once per frame
    void Update()
    {
        // Stores if the player is moving or not
        movementInput = Input.GetAxis("Horizontal");

        // Moves the player left or right
        rb.linearVelocity = new Vector3(movementInput * movementSpeed, rb.linearVelocity.y, rb.linearVelocity.z);

        // Rotates the player when changing direction
        if (movementInput != 0)
        {
            transform.rotation = Quaternion.LookRotation(new Vector3(movementInput, 0f, 0f));
            transform.Rotate(0f, 90f, 0f);
            isWalking = true;
        }
        // Otherwise face forward
        else
        {
            transform.rotation = Quaternion.LookRotation(new Vector3(-1f, 0f, 0f));
            isWalking = false;
        }

        if (isGrounded)
        {
            isJumping = false;
        }
        else
        {
            isJumping = true;
        }

        // Allows the player to switch between characters if there is enough space
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            if (!hitbox.GetComponent<HitboxScript>().isColliding)
            {
                Switch();
                //switchSound.Play();
            }
            else
            {
                //switchFailSound.Play();
            }
        }

        // Adjusts the player's variables and animations based on the character
        if (isBerry)
        {
            rb.mass = berryMass;
            jumpSpeed = berryJumpSpeed;
            berryAnimator.SetBool("isWalking", isWalking);
            berryAnimator.SetBool("isJumping", isJumping);
        }
        else if (isCarrot)
        {
            rb.mass = carrotMass;
            jumpSpeed = carrotJumpSpeed;
            carrotAnimator.SetBool("isWalking", isWalking);
            carrotAnimator.SetBool("isJumping", isJumping);
        }

        // Handles player jumping
        if (Input.GetButtonDown("Jump") && canJump)
        {
            rb.AddForce(transform.up * jumpSpeed, ForceMode.Impulse);
            canJump = false;
        }

        // Unlocks the next checkpoint when the player passes it
        if (transform.position.x > checkpoints[1].transform.position.x)
        {
            currentCheckpoint = checkpoints[1];
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

    // Switches the character
    public void Switch()
    {
        isBerry = !isBerry;
        isCarrot = !isCarrot;
        berry.SetActive(isBerry);
        carrot.SetActive(isCarrot);
    }

    // Moves the player to the spawn point and removes one of the player's lives
    public void Respawn()
    {
        transform.position = currentCheckpoint.position;
        transform.rotation = currentCheckpoint.rotation;
        rb.linearVelocity = new Vector3(0f, 0f, 0f);

        if (isBerry)
        {
            Switch();
        }
    }
}