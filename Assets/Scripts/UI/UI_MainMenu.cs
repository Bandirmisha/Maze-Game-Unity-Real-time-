using UnityEngine;
using UnityEngine.UI;

public class UI_MainMenu : MonoBehaviour
{
    public void StartGame(int id)
    {
        SceneLoader.LoadScene(id);
    }

    public void ExitGame()
    { 
        SceneLoader.ExitGame();
    }

}
