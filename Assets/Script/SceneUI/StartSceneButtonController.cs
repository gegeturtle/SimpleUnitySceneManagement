using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class StartSceneButtonController : MonoBehaviour
{
    Button StartButton;
    Button ExitButton;
    private void Start()
    {
        StartButton = GetComponent<UIDocument>().rootVisualElement.Q<Button>("StartButton");
        ExitButton = GetComponent<UIDocument>().rootVisualElement.Q<Button>("ExitButton");

        StartButton.clicked += ExecuteStart;
        ExitButton.clicked += ExecuteExit;
    }
    void ExecuteStart()
    {
        SceneManager.LoadScene("DifficultySelectionScene",LoadSceneMode.Single);
    }
    void ExecuteExit()
    {
        Application.Quit();
    }
}
