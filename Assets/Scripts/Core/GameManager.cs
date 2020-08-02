using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    GameMode gameMode;
    bool newModeHasBeenSet = false;

    private void Awake()
    {
        gameMode = GameMode.Regular;
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

    private void SetDoubleTwoSettings()
    {
        //Set the score from each fruit

        //Set the score from each path

        newModeHasBeenSet = false;
    
    }

    private void SetRegularSettings()
    {
        //Set the score from each fruit

        //Set the score from each path

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

}
