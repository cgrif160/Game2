using UnityEngine;

// Changes the color of the block when it reaches the desired x position
public class BlockScript : MonoBehaviour
{
    public float destinationX = 0;
    public GameObject block;
    public Material purple;
    public Material orange;

    private Material blockColor;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        blockColor = block.GetComponent<MeshRenderer>().material;
    }

    // Update is called once per frame
    void Update()
    {
        // Changes the color of the block depeneding on its x position
        if (block.transform.position.x >= destinationX)
        {
            blockColor = orange;
        }
        else
        {
            blockColor = purple;
        }
    }
}