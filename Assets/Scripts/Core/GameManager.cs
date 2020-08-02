using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    GameMode gameMode;

    private void Awake()
    {
        gameMode = GameMode.Regular;
    }

    public void SetGameMode(GameMode gameMode)
    {
        this.gameMode = gameMode;
    }

    public GameMode GetMode()
    {
        return this.gameMode;
    }

}
