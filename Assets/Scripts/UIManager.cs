using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{

    public void SwitchCanvasForSeconds(Canvas canvas,float time)
    {
        StartCoroutine(SwitchCanvasForSecondsCoroutine(canvas,time));
    }
   
    private IEnumerator SwitchCanvasForSecondsCoroutine(Canvas canvas,float time)
    {
        canvas.sortingOrder = 2;
        yield return new WaitForSeconds(time);
        canvas.sortingOrder = 0;

    }
}
