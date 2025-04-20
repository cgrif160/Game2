using UnityEngine;

// Makes sure the spring still looks good when it's being used
public class SpringScript : MonoBehaviour
{
    public GameObject top;
    public GameObject spring;
    public Material purple;
    public Material orange;
    public float topLimit;
    public float bottomLimit;
    public float changeColorY;

    private Rigidbody rb;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        top.GetComponent<MeshRenderer>().material = purple;
        rb = top.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        // Prevents the top of the spring from going too far up
        if (top.transform.localPosition.y > topLimit)
        {
            rb.linearVelocity = Vector3.zero;
        }

        // Prevents the top of the spring from phasing into the bottom
        if (top.transform.localPosition.y < bottomLimit)
        {
            top.transform.localPosition = new Vector3(top.transform.localPosition.x, bottomLimit, top.transform.localPosition.z);
        }

        // Changes the color of the top of the spring depending on how much it's being pressed down
        if (top.transform.localPosition.y < changeColorY)
        {
            top.GetComponent<MeshRenderer>().material = orange;
        }
        else
        {
            top.GetComponent<MeshRenderer>().material = purple;
        }

        // Squeezes the spring quad when the spring is being pressed down
        spring.transform.localScale = new Vector3(spring.transform.localScale.x, top.transform.localPosition.y + 0.125f, spring.transform.localScale.z);
        spring.transform.localPosition = new Vector3(spring.transform.localPosition.x, (top.transform.localPosition.y - 0.375f) / 2, spring.transform.localPosition.z);
    }
}