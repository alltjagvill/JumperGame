﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DeadMenu : MonoBehaviour
{

    
    

    
    
    public void RestartLevel()
    {
        string currentScene = SceneManager.GetActiveScene().name;
        SceneManager.LoadScene(currentScene);
        gameObject.SetActive(false);
    }

    public void StartMenu()
    {
        SceneManager.LoadScene("StartMenu");
    }

    public void QuitGame()
    {
        Application.Quit();
        Debug.Log("Quit Game!");
    }
}
