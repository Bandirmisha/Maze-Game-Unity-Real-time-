using UnityEngine;

public class SceneLoader : MonoBehaviour
{
    public static void LoadScene(int id)
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(id);
    }

    public static void ExitGame()
    {
        Application.Quit();
    }
}