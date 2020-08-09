using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

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

    public void RestartScene()
    {
        SceneManager.LoadScene(currentSceneIndex);
    }

    public void LoadMainMenu()
    {
        SceneManager.LoadScene("Start Screen");
    }

    public void LoadNextScene()
    {
        SceneManager.LoadScene(currentSceneIndex + 1);
    }
    public void LoadSpecificScene(int specificSceneIndex)
    {
        SceneManager.LoadScene(specificSceneIndex);
    }


    public void QuitGame()
    {
        Application.Quit();
    }





}
