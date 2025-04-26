using UnityEngine;
using UnityEngine.SceneManagement;

// Plays the correct music from scene to scene
public class MusicScript : MonoBehaviour
{
    public AudioSource ambienceLoop;
    public AudioSource musicLoop;
    public AudioSource winSound;

    // Start is called once before the first execution of Update after the MonoBehaviour is created    
    private void Start()
    {
        DontDestroyOnLoad(gameObject);
    }

    void OnEnable()
    {
        SceneManager.sceneLoaded += OnLevelFinishedLoading;
    }

    void OnDisable()
    {
        SceneManager.sceneLoaded -= OnLevelFinishedLoading;
    }

    void OnLevelFinishedLoading(Scene scene, LoadSceneMode mode)
    {
        if (scene.buildIndex == 0)
        {
            musicLoop.Stop();
            ambienceLoop.Play();
        }

        if (scene.buildIndex == 1)
        {
            ambienceLoop.Stop();
            musicLoop.Play();
        }

        if (scene.buildIndex == SceneManager.sceneCountInBuildSettings - 1)
        {
            winSound.Play();
        }
    }
}