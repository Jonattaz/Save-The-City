using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    // Controles Canvas
    public GameObject controles;

    // Menu Canvas
    public GameObject menu;

    // Credits 
    public GameObject credits;

    //Cheat Controller
    public static bool cheat = false;

    [SerializeField]
    // Musica que toca no menu
    private AudioClip menuSoundtrack;

    private void Start()
    {
        AudioManager.Instance.PlayMusic(menuSoundtrack);
    }

    // Load another scenes
    public void SceneLoad(int Scene)
    {
        SceneManager.LoadScene(Scene);
        
    }

    // Allows the player to exit the game
    public void ExitGame()
    {
        Application.Quit();
    }


    // When the Options button is clicked, activate the options canvas and deactivate the menu canvas
    public void MenuToControles()
    {
        menu.SetActive(false);
        controles.SetActive(true);

    }


    // When the Menu button is clicked, activate the Menu canvas and deactivate the Options canvas
    public void ControlesToMenu()
    {
        controles.SetActive(false);
        menu.SetActive(true);
    }

    
    public void MenuToCredits()
    {
        menu.SetActive(false);
        credits.SetActive(true);
    }

    public void CreditsToMenu()
    {
        credits.SetActive(false);
        menu.SetActive(true);
    }


}


