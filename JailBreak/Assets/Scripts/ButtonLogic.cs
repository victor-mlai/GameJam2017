using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonLogic : MonoBehaviour {

    AudioManager audioManager;

	// Use this for initialization
	void Start ()
    {
        audioManager = GameObject.Find("AudioManager").GetComponent<AudioManager>();
	}
	
    public void ButtonHover()
    {
        if (audioManager != null)
        {
            audioManager.Play("ButtonHover");
        }
    }

    public void PlayGame()
    {
        Application.LoadLevel("Nyx");
    }

    public void MainMenu()
    {
        Application.LoadLevel("Main Menu");
    }

    public void Quit()
    {
        Application.Quit();
    }
}
