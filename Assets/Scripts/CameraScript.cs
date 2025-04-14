using UnityEngine;

// Moves the camera with the specified target within the specified limits
public class CameraScript : MonoBehaviour
{
    public Transform target;
    public float cameraOffestY;
    public float leftLimit;
    public float rightLimit;
    public float bottomLimit;
    public float topLimit;

    private float xPos;
    private float yPos;
    private float zPos;

    // Update is called once per frame
    void Update()
    {
        // Only follows the target if it exists
        if(target != null)
        {
            // Handles x position
            if (target.position.x < leftLimit)
            {
                xPos = leftLimit;
            }
            /*
            else if (target.position.x > rightLimit)
            {
                xPos = rightLimit;
            }
            */
            else
            {
                xPos = target.position.x;
            }

            // Handles y position
            if (target.position.y < bottomLimit)
            {
                yPos = bottomLimit + cameraOffestY;
            }
            else if (target.position.y > topLimit)
            {
                yPos = topLimit + cameraOffestY;
            }
            else
            {
                yPos = target.position.y + cameraOffestY;
            }

            // Sets constamt z position
            zPos = transform.position.z;

            // Moves the camera to the correct position
            transform.position = new Vector3(xPos, yPos, zPos);
        }
    }
}