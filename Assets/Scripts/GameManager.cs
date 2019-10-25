using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    
    public Camera mainCamera;
    public TextMeshPro timeCounterText;
    public GameObject deadMenuUI;
    public GameObject levlelCompleteUI;

    private RestartController restartController;

    private float secondsCount;
    private int minuteCount;

    private void OnEnable()
    {
        PlayerController.OnDangerHit += EnableDeadMenu;
        PlayerController.OnExitHit += EnableNextLevelMenu;
    }

    private void OnDisable()
    {
        PlayerController.OnDangerHit -= EnableDeadMenu;
        PlayerController.OnExitHit -= EnableNextLevelMenu;
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
    private void EnableDeadMenu()
    {
        deadMenuUI.SetActive(true);
    }

    private void EnableNextLevelMenu()
    {
        levlelCompleteUI.SetActive(true);
        Time.timeScale = 0f;
    }
}
