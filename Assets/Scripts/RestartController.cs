using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class RestartController : MonoBehaviour
{
    public Camera mainCamera;
    bool playerIsDead = false;

    
    void Update()
    {
        if (playerIsDead == true)
        {
            FollowMainCamera();
        }
    }
    private void OnMouseDown()
    {
        string currentScene = SceneManager.GetActiveScene().name;
        SceneManager.LoadScene(currentScene);

        
    }

    public void OnPlayerDead()
    {
        playerIsDead = true;
       
    }

    public void FollowMainCamera()
    {
        transform.position = mainCamera.transform.position;
        transform.position = new Vector3(transform.position.x, transform.position.y, 0);
    }
   
}
