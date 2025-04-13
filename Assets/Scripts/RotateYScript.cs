using UnityEngine;

// Makes its object spin around
public class RotateYScript : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        transform.Rotate(new Vector3(0f, 180f, 0f) * Time.deltaTime);
    }
}