using UnityEngine;

public class B_Collectible : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    //Checks if player collides with berry collectible
    void OnTriggerEnter(Collider other)
        {
            if(other.tag == "Player")
            {
                Player.Instance.AddBerries();
                Debug.Log("You got a berry!");
                Destroy(this.gameObject);
            }
        }
}
