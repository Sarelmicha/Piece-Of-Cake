using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    ShakeBehavior shake;
    GameManager gameManager;

    private void Start()
    {
        shake = Camera.main.transform.GetComponent<ShakeBehavior>();
        gameManager = GameObject.FindWithTag("Game Manager").GetComponent<GameManager>();
       
    }

    public void SwitchCanvasForSeconds(Canvas canvas,float time)
    {
        StartCoroutine(SwitchCanvasForSecondsCoroutine(canvas,time));
    }
   
    private IEnumerator SwitchCanvasForSecondsCoroutine(Canvas canvas,float time)
    {
        gameManager.SetGameMode(GameMode.DoubleTwo);
        shake.TriggerShake();
        canvas.sortingOrder = 2;
        yield return new WaitForSeconds(time);
        canvas.sortingOrder = 0;
        shake.TriggerShake();
        gameManager.SetGameMode(GameMode.Regular);

    }
}
