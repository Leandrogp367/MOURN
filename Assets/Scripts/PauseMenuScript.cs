using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenuScript : MonoBehaviour
{
    public static bool gamePaused = false;

    public GameObject pauseMenuUI;
    public GameObject confirmationScreen;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if(gamePaused)
            {
                ResumeGame();
            }
            else
            {
                PauseGame();
            }
        }
    }

    public void ResumeGame()
    {
        pauseMenuUI.SetActive(false);
        confirmationScreen.SetActive(false);
        Time.timeScale = 1f;
        gamePaused = false;
        Cursor.lockState = CursorLockMode.Locked;
    }
    public void PauseGame()
    {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        gamePaused = true;
        Cursor.lockState = CursorLockMode.None;
    }

    public void QuitGame()
    {
        Debug.Log("Quiting game...");
        pauseMenuUI.SetActive(false);
        confirmationScreen.SetActive(true);
    }

    public void ConfirmQuit()
    {
        Debug.Log("Quiting Confirmed");
        Application.Quit();
    }

    public void RefuseQuit()
    {
        Debug.Log("Quiting Refused");
        confirmationScreen.SetActive(false);
        pauseMenuUI.SetActive(true);
    }
}
