using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    [SerializeField] Fader fader;
    [SerializeField] float fadeTime = 0.1f;

    int currentSceneIndex = 0;
    int currentLevel;


    private void Awake()
    {

        if (FindObjectsOfType<GameManager>().Length > 1)
        {
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }

        //TODO - In the future get the current level from the file system
        currentSceneIndex = 0;
        currentLevel = 1;
    }

    public void LoadNextScene()
    {
        StartCoroutine(LoadNextSceneCoroutine());
    }


    private IEnumerator LoadMainMenu()
    {
        yield return fader.FadeOut(fadeTime);
        SceneManager.LoadScene("Start Screen");
        yield return fader.FadeIn(fadeTime);
    }


    public IEnumerator RestartScene()
    {
        yield return fader.FadeOut(fadeTime);
        SceneManager.LoadScene(currentSceneIndex);
        yield return fader.FadeIn(fadeTime);
    }
    private IEnumerator LoadNextSceneCoroutine()
    {
    
        yield return fader.FadeOut(fadeTime);
        SceneManager.LoadScene(currentSceneIndex + 1);
        yield return  fader.FadeIn(fadeTime);
    }
    private IEnumerator LoadSpecificScene(int specificSceneIndex)
    {
        yield return fader.FadeOut(fadeTime);
        SceneManager.LoadScene(specificSceneIndex);
        yield return fader.FadeIn(fadeTime);
    }


    public void QuitGame()
    {
        Application.Quit();
    }
}
