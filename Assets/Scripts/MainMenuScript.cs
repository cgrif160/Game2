using UnityEngine;
using UnityEngine.SceneManagement; 

// Manages the main menu
public class MainMenuScript : MonoBehaviour
{
    public AudioSource buttonPress;

    // Plays a sound for when a button is pressed
    public void ButtonPress()
    {
        //buttonPress.Play();
    }

    // Loads the first level
    public void StartGame()
    {
        SceneManager.LoadScene(1);
    }

    // Displays the controls
    public void Controls()
    {

    }

    // Quits the game
    public void Quit()
    {
        Application.Quit();
    }
}