using UnityEngine;

// Changes the color of the block when it reaches the desired x position
public class BlockScript : MonoBehaviour
{
    public float destinationX;
    public Vector3 spawn;
    public Material purple;
    public Material orange;

    private Rigidbody rb;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        GetComponent<MeshRenderer>().material = purple;
    }

    // Update is called once per frame
    void Update()
    {
        // Changes the color of the block depeneding on its x position
        if (transform.position.x >= destinationX)
        {
            GetComponent<MeshRenderer>().material = orange;
        }
        else
        {
            GetComponent<MeshRenderer>().material = purple;
        }

        // Respawwns the block if it falls beelow the level
        if (transform.position.y < -10)
        {
            transform.position = spawn;
            rb.linearVelocity = new Vector3(0f, 0f, 0f);
        }
    }
}