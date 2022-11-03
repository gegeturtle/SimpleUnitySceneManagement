using System;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;

public class DifficultySelectionController : MonoBehaviour
{
    Button HardButton;
    Button UltraHardButton;
    Button BackButton;
    private void Start()
    {
        HardButton = GetComponent<UIDocument>().rootVisualElement.Q<Button>("HardButton");
        UltraHardButton = GetComponent<UIDocument>().rootVisualElement.Q<Button>("UltraHardButton");
        BackButton = GetComponent<UIDocument>().rootVisualElement.Q<Button>("BackButton");

        HardButton.clicked += () =>
        {
            LoadScene(Difficulty.Hard);
        };
        UltraHardButton.clicked += () =>
        {
            LoadScene(Difficulty.UltraHard);
        };
        BackButton.clicked += ExecuteBack;
    }
    void LoadScene (Difficulty difficulty)
    {
        GameInitializationParameter.Difficulty = difficulty;
        SceneManager.LoadScene("GameScene",LoadSceneMode.Single);
    }
    void ExecuteBack()
    {
        SceneManager.LoadScene("StartScene",LoadSceneMode.Single);
    }
}
public enum Difficulty{
    none,
    Hard,
    UltraHard,
}
