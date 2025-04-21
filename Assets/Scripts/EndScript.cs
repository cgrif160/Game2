using UnityEngine;
using UnityEngine.SceneManagement; 

// Manages the end scene
public class EndScript : MonoBehaviour
{
    public AudioSource buttonPress;

    // Plays a sound for when a button is pressed
    public void ButtonPress()
    {
        //buttonPress.Play();
    }

    // Goes back to the main menu
    public void MainMenu()
    {
        SceneManager.LoadScene(0);
    }

    // Quits the game
    public void Quit()
    {
        Application.Quit();
    }
}