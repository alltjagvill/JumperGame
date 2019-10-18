using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    public GameObject restartScreen;
    public Camera mainCamera;
    public TextMeshPro timeCounterText;

    private RestartController restartController;

    private float secondsCount;
    private int minuteCount;

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

    private void Update()
    {
        UpdateTimeCounter();
    }

    private void UpdateTimeCounter()
    {
        secondsCount += Time.deltaTime;
        timeCounterText.text = minuteCount + "m:" + (int)secondsCount + "s";

        if (secondsCount >= 60)
        {
            minuteCount++;
            secondsCount = 0;
        }
    }
    private void EnableRestartScreen()
    {


        restartController.OnPlayerDead();
        restartScreen.SetActive(true);

        Debug.Log("RESTARTSCREEN");
    }

}
