using UnityEngine;
using UnityEngine.SceneManagement; 

// Manages the main menu
public class MainMenuScript : MonoBehaviour
{
    public GameObject title;
    public GameObject controls;
    public AudioSource buttonPress;

    private bool isControls = false;

    // Update is called once per frame
    void Update()
    {
        // Allows the player to close the controls screen with escape
        if (Input.GetKeyDown(KeyCode.Escape) && isControls)
        {
            Back();
        }
    }
        
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
        isControls = !isControls;
        title.SetActive(!isControls);
        controls.SetActive(isControls);
    }

    // Goes back to the title screen
    public void Back()
    {
        isControls = false;
        title.SetActive(!isControls);
        controls.SetActive(isControls);
    }

    // Quits the game
    public void Quit()
    {
        Application.Quit();
    }
}