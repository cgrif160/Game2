using UnityEngine;

// Makes sure the spring still looks good when it's being used
public class SpringScript : MonoBehaviour
{
    public GameObject top;
    public GameObject spring;
    public Material purple;
    public Material orange;

    private Material topColor;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        topColor = top.GetComponent<MeshRenderer>().material;
    }

    // Update is called once per frame
    void Update()
    {
        // Prevents the top of the spring from phasing into the bottom
        if (top.transform.localPosition.y < -0.125f)
        {
            top.transform.localPosition = new Vector3(top.transform.localPosition.x, -0.125f, top.transform.localPosition.z);
        }

        // Changes the color of the top of the spring depending on how much it's being pressed down
        if (top.transform.localPosition.y < 0.2f)
        {
            topColor = orange;
        }
        else
        {
            topColor = purple;
        }

        // Squeezes the spring quad when the spring is being pressed down
        spring.transform.localScale = new Vector3(spring.transform.localScale.x, ((top.transform.localPosition.y + 0.125f) * 2), spring.transform.localScale.z);
    }
}