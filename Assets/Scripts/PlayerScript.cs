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
    public GameObject respawnButton;
    public GameObject controlsButton;
    public GameObject mainMenuButton;
    public GameObject quitButton;
    public GameObject controls;
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
    private bool isControls = false;
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
        isControls = false;
        Time.timeScale = 0;
        pauseScreen.SetActive(true);
        respawnButton.SetActive(!isControls);
        controlsButton.SetActive(!isControls);
        mainMenuButton.SetActive(!isControls);
        quitButton.SetActive(!isControls);
        controls.SetActive(isControls);
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
        isControls = true;
        respawnButton.SetActive(!isControls);
        controlsButton.SetActive(!isControls);
        mainMenuButton.SetActive(!isControls);
        quitButton.SetActive(!isControls);
        controls.SetActive(isControls);
    }

    // Goes back to the pause screen
    public void Back()
    {
        isControls = false;
        respawnButton.SetActive(!isControls);
        controlsButton.SetActive(!isControls);
        mainMenuButton.SetActive(!isControls);
        quitButton.SetActive(!isControls);
        controls.SetActive(isControls);
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