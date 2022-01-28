using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{

    public static bool gameIsPaused = false;

    public GameObject pauseMenuUI;




    // Update is called once per frame
    void Update()
    {

        if (gameIsPaused && pauseMenuUI != null)
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
        else
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }



        if (Input.GetKeyDown(KeyCode.Escape))
        {


            if (gameIsPaused)
            {

                Resume();

            }
            else
            {
                Pause();
            }
        }

    }


    public void Resume()
    {

        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        gameIsPaused = false;

    }

    void Pause()
    {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        gameIsPaused = true;

    }

    public void QuitGame()
    {

        Debug.Log("Quit");
        Application.Quit();
    }
}

