using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class UIPause : MonoBehaviour
{
   
    public GameObject panelPause;


    public void Pause()
    {
        Time.timeScale = 0;
        panelPause.SetActive(true);
    }
    public void Retry()
    {
        Time.timeScale = 1;
        panelPause.SetActive(false);
    }
    public void Menu()
    {
        SceneManager.LoadScene("Menu");
        Time.timeScale = 1;
    }
}
