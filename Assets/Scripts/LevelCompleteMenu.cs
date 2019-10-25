using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelCompleteMenu : MonoBehaviour
{

    public int nrOfNonLevelScenes = 1;
    
    

    public void NextLevel()
    {
        int sceneNumber = SceneManager.GetActiveScene().buildIndex;
        Debug.Log("Scenenumber: " + sceneNumber);
        Debug.Log("Number of scenes: " + SceneManager.sceneCountInBuildSettings);
        if (sceneNumber >= SceneManager.sceneCountInBuildSettings - nrOfNonLevelScenes)
        {
            SceneManager.LoadScene("StartMenu");
            Time.timeScale = 1f;
            
        }
        else
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
            Time.timeScale = 1f;
        }
    }
}
