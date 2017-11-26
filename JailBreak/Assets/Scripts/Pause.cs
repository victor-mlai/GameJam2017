using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Pause : MonoBehaviour {

    AudioManager audioManager;
    Text text;

    GameObject b_resume;
    GameObject b_goToMainMenu;
    GameObject b_quit;
    GameObject b_yesMain;
    GameObject b_noMain;
    GameObject b_yesQuit;
    GameObject b_noQuit;

    // Use this for initialization
    void Start () {
        audioManager = GameObject.Find("AudioManager").GetComponent<AudioManager>();
        text = gameObject.transform.Find("Title").gameObject.GetComponent<Text>();

        b_resume = gameObject.transform.Find("Resume").gameObject;
        b_goToMainMenu = gameObject.transform.Find("Go To Main Menu").gameObject;
        b_quit = gameObject.transform.Find("Quit").gameObject;

        b_yesMain = gameObject.transform.Find("Yes - Main Menu").gameObject;
        b_noMain = gameObject.transform.Find("No - Main Menu").gameObject;

        b_yesQuit = gameObject.transform.Find("Yes - Quit").gameObject;
        b_noQuit = gameObject.transform.Find("No - Quit").gameObject;

        text.text = "Pause";
    }

    public void ButtonHover()
    {
        if (audioManager != null)
        {
            audioManager.Play("ButtonHover");
        }
    }

    public void Resume()
    {
        Time.timeScale = 1;
        gameObject.SetActive(false);
    }

    public void GoToMainMenu()
    {
        //Application.LoadLevel("Main Menu");
        text.text = "Are you sure you want to return to Main Menu?";

        TogglePauseButtons();
        ToggleMainButtons();
    }

    public void Quit()
    {
        text.text = "Are you sure you want to quit?";

        TogglePauseButtons();
        ToggleQuitButtons();
    }

    public void YesMainMenu()
    {
        Application.LoadLevel("Main Menu");
    }

    public void NoMainMenu()
    {
        //Application.LoadLevel("Main Menu");
        text.text = "Pause";

        TogglePauseButtons();
        ToggleMainButtons();
    }

    public void YesQuit()
    {
        Application.Quit();
    }

    public void NoQuit()
    {
        //Application.LoadLevel("Main Menu");
        text.text = "Pause";

        TogglePauseButtons();
        ToggleQuitButtons();
    }


    void TogglePauseButtons()
    {
        b_resume.SetActive(!b_resume.active);
        b_goToMainMenu.SetActive(!b_goToMainMenu.active);
        b_quit.SetActive(!b_quit.active);
    }

    void ToggleMainButtons()
    {
        b_yesMain.SetActive(!b_yesMain.active);
        b_noMain.SetActive(!b_noMain.active);
    }

    void ToggleQuitButtons()
    {
        b_yesQuit.SetActive(!b_yesQuit.active);
        b_noQuit.SetActive(!b_noQuit.active);
    }
}
