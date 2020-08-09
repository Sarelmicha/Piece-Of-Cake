using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    [SerializeField] Text scoreText;
    [SerializeField] Text timeText;
    [SerializeField] Progression progression;

    ShakeBehavior shake;
    int currentLevel;
    GameMode gameMode;

    int score = 0;
    int timer = 10;


    private void Awake()
    {
        shake = Camera.main.transform.GetComponent<ShakeBehavior>();
    }

    private void Start()
    {
        gameMode = GameMode.Regular;
        scoreText.text = score.ToString();
        timeText.text = timer.ToString();
        StartCoroutine(UpdateTime());

    }

    private void Update()
    {
        if (timer == 0)
        {
            FinishLevel();
        }
    }

    public void SwitchCanvasForSeconds(Canvas canvas, float time)
    {
        StartCoroutine(SwitchCanvasForSecondsCoroutine(canvas, time));
    }

    private IEnumerator SwitchCanvasForSecondsCoroutine(Canvas canvas, float time)
    {
        SetGameMode(GameMode.DoubleTwo);
        shake.TriggerShake();
        canvas.sortingOrder = 2;
        yield return new WaitForSeconds(time);
        canvas.sortingOrder = 0;
        shake.TriggerShake();
        SetGameMode(GameMode.Regular);

    }

    private void FormatTime()
    {
        int minutes = Mathf.FloorToInt(timer / 60f);
        int seconds = Mathf.FloorToInt(timer - minutes * 60);
        timeText.text = string.Format("{0:0}:{1:00}", minutes, seconds);
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

    public void SetGameMode(GameMode gameMode)
    {
        this.gameMode = gameMode;
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

    private void FinishLevel()
    {
        CalculateStars();
    }


    private void CalculateStars()
    {

        if (score < progression.GetScore(Stat.ScoreFor2Star, currentLevel))
        {
            print("you get 1 star");
        }

        else if (score >= progression.GetScore(Stat.ScoreFor2Star, currentLevel) && score < progression.GetScore(Stat.ScoreFor3Star, currentLevel))
        {
            print("you get 2 starts");
        }

        else if (score >= progression.GetScore(Stat.ScoreFor3Star, currentLevel))
        {
            print("you get 3 stars!");
        }

    }
}
