using UnityEngine;
using UnityEngine.EventSystems;

// Handles the player's movement
public class Player : MonoBehaviour
{
    public GameObject spawn;
    public GameObject berry;
    public GameObject carrot;
    public int lives;
    public int carrots;
    public int berries;

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
    private static Player _instance;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        carrots = 0;
        berries = 0;
    }

    //Creates one instance of script
    public static Player Instance
    {
        get
        {
            if(_instance == null)
            {
                Debug.LogError("Player is null");
            }
            return _instance;
        }
    }
    void Awake()
    {
        _instance = this;
    }

    public void AddCarrots()
    {
        carrots++;
    }
    public void AddBerries()
    {
        berries++;
    }

    // Update is called once per frame
    void Update()
    {
        // moves the player left or right
        rb.linearVelocity = new Vector3(Input.GetAxis("Horizontal") * movementSpeed, rb.linearVelocity.y, rb.linearVelocity.z);

        //rotates the player when changing direction
        if (Input.GetAxis("Horizontal") != 0)
        {
            transform.rotation = Quaternion.LookRotation(new Vector3(Input.GetAxis("Horizontal"), 0f, 0f));
            transform.Rotate(0f, 90f, 0f);
        }

        // allows the player to switch between characters
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            isBerry = !isBerry;
            isCarrot = !isCarrot;
            berry.SetActive(isBerry);
            carrot.SetActive(isCarrot);
        }

        // adjusts the player's variables based on the character
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
        UIManager.Instance.UpdateCarrotsText(carrots);
        UIManager.Instance.UpdateBerriesText(berries);
    }

    // Called if the player collides with something
    void OnCollisionEnter(Collision collision)
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