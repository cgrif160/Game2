using UnityEngine;

// Makes its object spin around
public class ClawScript : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        while (transform.rotation.z < 45)
        {
            transform.Rotate(new Vector3(0f, 0f, 5f) * Time.deltaTime);
        }
    }
}