using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fader : MonoBehaviour
{

    [SerializeField] RectTransform rectTransform;
    Coroutine currentActiveFade = null;

    private void Awake()
    {
        if (FindObjectsOfType<Fader>().Length > 1)
        {
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }
    }

    public Coroutine FadeOut(float time)
    {
   
        return Fade(new Vector3(12, 12, 0), time);
    }

    public Coroutine FadeIn(float time)
    {
        return Fade(Vector3.zero, time);
    }

    public Coroutine Fade(Vector3 target, float time)
    {
        //Cancel any running coroutine
        if (currentActiveFade != null)
        {
            StopCoroutine(currentActiveFade);
        }


        //Run the fade out coroutine
        currentActiveFade = StartCoroutine(FadeRoutine(target, time));
        return currentActiveFade;
    }

    private IEnumerator FadeRoutine(Vector3 target , float time)
    {
        while (!Mathf.Approximately(rectTransform.localScale.x, target.x))
        {
            rectTransform.localScale = new Vector3(Mathf.MoveTowards(rectTransform.localScale.x, target.x, Time.deltaTime / time), 
                Mathf.MoveTowards(rectTransform.localScale.y, target.y, Time.deltaTime / time),0);

            yield return new WaitForSeconds(0.01f);
        }
    }
}
