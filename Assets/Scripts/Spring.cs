using UnityEngine;

// Changes the color of the spring when its interacted with
public class Spring : MonoBehaviour
{
    public GameObject top;
    public Material purple;
    public Material orange;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        top.GetComponent<MeshRenderer>().material = purple;
    }

    // Update is called once per frame
    void Update()
    {
        // Prevents the top of the spring from phasing into the bottom
        if (top.transform.position.y < -0.18f)
        {
            top.transform.position = new Vector3(top.transform.position.x, -0.18f, top.transform.position.z);
        }

        if (top.transform.position.y < -0.1f)
        {
            top.GetComponent<MeshRenderer>().material = orange;
        }
        else
        {
            top.GetComponent<MeshRenderer>().material = purple;
        }
    }
}