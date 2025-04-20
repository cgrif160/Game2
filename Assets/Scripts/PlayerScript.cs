using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

// Handles the player's movement and interactions
public class PlayerScript : MonoBehaviour
{
    public List<GameObject> checkpoints = new List<GameObject>();
    public GameObject berry;
    public GameObject carrot;
    public GameObject hitbox;
    public GameObject pauseScreen;
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
    public AudioSource buttonPress;

    private Rigidbody rb;
    private float movementInput;
    private bool isGrounded;
    private bool isWalking;
    private bool isJumping;
    private bool canJump;
    private float jumpSpeed;
    private bool isPaused = false;
    private bool isBerry = false;
    private bool isCarrot = true;
    private int berriesCount = 0;
    private int carrotsCount = 0;
    private GameObject currentCheckpoint;

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

        // Respawns the player if they fall below the level
        if (transform.position.y < -10)
        {
            Respawn();
        }

        // Allows the player to pause the game
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            isPaused = !isPaused;

            if (isPaused)
            {
                Pause();
            }
            else
            {
                Unpause();
            }
        }

        // Goes to the next scene when the player reaches the last checkpoint
        if (currentCheckpoint == checkpoints[checkpoints.Count - 1])
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }

    //  Called if the player collides with a trigger
    void OnTriggerEnter(Collider collision)
    {
        // If a player collides with a checkpoint, make that the current checkpoint
        if (collision.gameObject.tag == "Checkpoint")
        {
            currentCheckpoint = collision.gameObject;
        }

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
        transform.position = currentCheckpoint.transform.position;
        transform.rotation = currentCheckpoint.transform.rotation;
        rb.linearVelocity = new Vector3(0f, 0f, 0f);

        if (isBerry)
        {
            Switch();
        }
    }

    // Pauses the game
    public void Pause()
    {
        isPaused = true;
        Time.timeScale = 0;
        pauseScreen.SetActive(true);
    }

    // Unpauses the game
    public void Unpause()
    {
        isPaused = false;
        Time.timeScale = 1;
        pauseScreen.SetActive(false);
    }

    // Plays a sound for when a button is pressed
    public void ButtonPress()
    {
        //buttonPress.Play();
    }

    // Displays the controls
    public void Controls()
    {

    }

    // Goes back to the main menu
    public void MainMenu()
    {
        SceneManager.LoadScene(0);
    }

    // Quits the game
    public void Quit()
    {
        Application.Quit();
    }
}