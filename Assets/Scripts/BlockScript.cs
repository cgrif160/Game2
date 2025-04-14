using UnityEngine;

// Changes the color of the block when it reaches the desired x position
public class BlockScript : MonoBehaviour
{
    public float destinationX;
    public Vector3 spawn;
    public GameObject block;
    public Material purple;
    public Material orange;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        block.GetComponent<MeshRenderer>().material = purple;
    }

    // Update is called once per frame
    void Update()
    {
        // Changes the color of the block depeneding on its x position
        if (transform.position.x >= destinationX)
        {
            block.GetComponent<MeshRenderer>().material = orange;
        }
        else
        {
            block.GetComponent<MeshRenderer>().material = purple;
        }

        // Respawwns the block if it falls beelow the level
        if (transform.position.y < -10)
        {
            transform.position = spawn;
        }
    }
}