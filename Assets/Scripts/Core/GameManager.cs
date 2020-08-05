using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{

    [SerializeField] Text scoreText;
    [SerializeField] Text timeText;

    int score = 0;
    int timer = 120;

    GameMode gameMode;
    bool newModeHasBeenSet = false;
 

    private void Awake()
    {
        gameMode = GameMode.Regular;
    }

    private void Start()
    {
        scoreText.text = score.ToString();
        timeText.text = timer.ToString();
        StartCoroutine(UpdateTime());
    }

    private void Update()
    {
        if (!newModeHasBeenSet)
        {
            return;
        }

        switch (gameMode)
        {
            case GameMode.Regular:
                SetRegularSettings();
                break;
            case GameMode.DoubleTwo:
                SetDoubleTwoSettings();
                break;
        }

      
    }

    private IEnumerator UpdateTime()
    {
        while (timer > 0)
        {
            yield return new WaitForSeconds(1);
            timer--;
            FormatTime();

        }
      
    }

    private void FormatTime()
    {
        int minutes = Mathf.FloorToInt(timer / 60f);
        int seconds = Mathf.FloorToInt(timer - minutes * 60);
        timeText.text = string.Format("{0:0}:{1:00}", minutes, seconds);
    }

    private void SetDoubleTwoSettings()
    {
        newModeHasBeenSet = false;
    
    }

    private void SetRegularSettings()
    {
        newModeHasBeenSet = false;

    }

    public void SetGameMode(GameMode gameMode)
    {
        this.gameMode = gameMode;
        newModeHasBeenSet = true;
    }

    public GameMode GetGameMode()
    {
        return this.gameMode;
    }

    public void AddToScore(int scorePoints)
    {
        if (gameMode == GameMode.Regular)
        {
            UpdateScore(scorePoints);
        }
        else if (gameMode == GameMode.DoubleTwo)
        {
            UpdateScore(scorePoints * 2);
        }
    }

    private void UpdateScore(int scorePoints)
    {
        score += scorePoints;
        if (score < 0)
        {
            score = 0;
        }
        scoreText.text = score.ToString();
    }

}
