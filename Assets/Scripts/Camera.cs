using Unity.VisualScripting;
using UnityEngine;

// Moves the camera with the specified target within the specified area
public class Camera : MonoBehaviour
{
    public Transform target;
    public float leftLimit;
    public float rightLimit;
    public float topLimit;
    public float bottomLimit;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(target != null)
        {
            if (target.position.y > 2.5)
            {
                if (target.position.x < -11.65)
                {
                    transform.position = new Vector3(-11.65f, (target.position.y - 2.5f), transform.position.z);
                }
                else if (target.position.x > 74.25)
                {
                    transform.position = new Vector3(74.25f, (target.position.y - 2.5f), transform.position.z);
                }
                else
                {
                    transform.position = new Vector3(target.position.x, (target.position.y - 2.5f), transform.position.z);
                }
            }
            else if (target.position.y < -3)
            {
                transform.position = new Vector3(0, -13, -1);
            }
            else
            {
                if (target.position.x < -11.65)
                {
                    transform.position = new Vector3(-11.65f, 0, transform.position.z);
                }
                else if (target.position.x > 74.25)
                {
                    transform.position = new Vector3(74.25f, 0, transform.position.z);
                }
                else
                {
                    transform.position = new Vector3(target.position.x, 0, transform.position.z);
                }
            }
        }
    }
}