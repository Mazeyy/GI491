using System.Collections;
using System.Collections.Generic;
using Unity.IO.LowLevel.Unsafe;
using UnityEditor.Animations;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    public static bool GameIsPaused = false;
    public GameObject pauseMenuUI;
    public GameObject resumeBtn;
    public GameObject settingBtn;
    public GameObject GamePauseImage;
    public GameObject exitBtn;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (GameIsPaused)
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
        GameIsPaused = false;
        resumeBtn.SetActive(false);
        settingBtn.SetActive(false);
        GamePauseImage.SetActive(false);
        exitBtn.SetActive(false);
    }

    public void Pause()
    {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        GameIsPaused = true;
        resumeBtn.SetActive(true);
        settingBtn.SetActive(true);
        GamePauseImage.SetActive(true);
        exitBtn.SetActive(true);
    }

    public void Exit()
    {
        Debug.Log("Quit");
        Application.Quit();
    }
}
