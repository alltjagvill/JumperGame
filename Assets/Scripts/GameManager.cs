using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject restartScreen;
    public Camera mainCamera;
    private RestartController restartController;

    private void OnEnable()
    {
        PlayerController.OnDangerHit += EnableRestartScreen;
    }

    private void OnDisable()
    {
        PlayerController.OnDangerHit -= EnableRestartScreen;
    }

    void Start()
    {
        restartController = restartScreen.GetComponent<RestartController>();
    }

    private void EnableRestartScreen()
    {


        restartController.OnPlayerDead();
        restartScreen.SetActive(true);

        Debug.Log("RESTARTSCREEN");
    }

}
