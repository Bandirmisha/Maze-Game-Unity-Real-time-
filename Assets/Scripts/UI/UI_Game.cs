using MazeGame;
using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UI_Game : MonoBehaviour
{
    public Action onPlayerHealthChanged;
    public Action onQuestChanged;
    private static Player player => GameManager.instance.player.GetComponent<Player>();

    [SerializeField] private TextMeshProUGUI healthTextBox;
    [SerializeField] private TextMeshProUGUI questTextBox;

    [SerializeField] private GameObject PauseMenu;
    [SerializeField] private GameObject GameEndMenu;

    private void OnEnable()
    {
        onPlayerHealthChanged += DrawStats;
        onQuestChanged += DrawStats;
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            PauseMenuSwitchView();
        }
    }

    public void DrawStats()
    {
        healthTextBox.text = "Здоровье: " + player.HP;
        questTextBox.text = "Задание: " + player.Quest;
    }

    public void PauseMenuSwitchView()
    {
        PauseMenu.SetActive(!PauseMenu.activeSelf);
    }

    public void ShowGameEndMenu()
    {
        GameEndMenu.SetActive(true);
    }

    public void LoadScene(int id)
    {
        SceneLoader.LoadScene(id);
    }

}
