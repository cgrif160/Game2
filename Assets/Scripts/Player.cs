using UnityEngine;

// Handles the player's movement
public class Player : MonoBehaviour
{
    public GameObject spawn;
    public float movementSpeed = 3;
    public float rotationSpeed = 3;
    public int lives = 3;

    private Rigidbody rb;
    private Vector3 movement;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        // Respawns the player if they fall below the level
        if (transform.position.y < -10)
        {
            Respawn();
        }
    }

    // Fixed update is called once per physics frame
    void FixedUpdate()
    {
        movement = new Vector3((Input.GetAxis("Horizontal") + 0.001f) * movementSpeed, 0, 0);
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(movement), rotationSpeed);
        transform.Translate(movement * movementSpeed * Time.deltaTime, Space.World);
    }

    // Called if the object collides with something
    void OnCollisionEnter(Collision collision)
    {
        // Respawns the player if they collide with an enemy
        if (collision.gameObject.tag == "Enemy")
        {
            Respawn();
        }
    }

    // Moves the player to the spawn point and removes one of the player's lives
    public void Respawn()
    {
        transform.position = spawn.transform.position;
        transform.rotation = spawn.transform.rotation;
        rb.linearVelocity = new Vector3(0, 0, 0);
        lives -= 1;
    }
}