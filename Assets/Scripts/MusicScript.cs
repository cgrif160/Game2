using UnityEngine;
using UnityEngine.SceneManagement;

// Plays background music from scene to scene
public class MusicScript : MonoBehaviour
{
    public MusicScript instance;

    // Start is called once before the first execution of Update after the MonoBehaviour is created    
    void Start()
    {
        if(instance == null)
        {
            if (SceneManager.GetActiveScene().buildIndex < 1)
            {
                instance = this;
                DontDestroyOnLoad(gameObject);
            }
        }
        else
        {
            Destroy(instance);
        }

        AudioSource music = GetComponent<AudioSource>();
        music.Play();
    }
}
