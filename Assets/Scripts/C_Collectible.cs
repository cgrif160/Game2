using UnityEngine;

public class C_Collectible : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //Checks if player collides with carrot collectible
    void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            Player.Instance.AddCarrots();
            Debug.Log("You got a carrot!");
            Destroy(this.gameObject);
        }
    }
}
