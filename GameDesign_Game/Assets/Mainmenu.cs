using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Mainmenu : MonoBehaviour
{
    public GameObject SettingWindow;
    public GameObject MusicImage;
    public GameObject EffectImage;
    public GameObject SaveBtn;
    public void PlayGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void QuitGame()
    {
        Debug.Log("Quit");
        Application.Quit();
    }

    public void Setting()
    {
        SettingWindow.SetActive(true);
        MusicImage.SetActive(true);
        EffectImage.SetActive(true);
        SaveBtn.SetActive(true);
    }
    public void Save()
    {
        SettingWindow.SetActive(false);
        MusicImage.SetActive(false);
        EffectImage.SetActive(false);
        SaveBtn.SetActive(false);
        
    }
}
