using UnityEngine;

// Makes its object jump up and down
public class JumpScript : MonoBehaviour
{
    public float startX;
    public float jumpSpeed;

    private Rigidbody rb;
    private bool canJump;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }
    
    // Update is called once per frame
    void FixedUpdate()
    {
        if (transform.position.y < startX)
        {
            rb.linearVelocity = Vector3.zero;
            rb.AddForce(transform.up * jumpSpeed, ForceMode.Impulse);
        }
    }
}