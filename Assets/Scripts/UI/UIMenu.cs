using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Events;
public class UIMenu : MonoBehaviour
{
     public static UnityEvent ResetStats = new UnityEvent();
    public void Play()
    {
        ResetStats.Invoke();
        SceneManager.LoadScene("Game");
    }
    public void Credits()
    {
         SceneManager.LoadScene("Credits");
    }
    public void Exit()
    {
        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
        #else
        Application.Quit(); 
        #endif
    }
}
