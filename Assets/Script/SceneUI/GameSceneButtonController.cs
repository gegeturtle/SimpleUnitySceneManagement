using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using Gege;

public class GameSceneButtonController : MonoBehaviour
{
    Button StartButton;
    Button ExitButton;
    VisualElement HealthBar;
    VisualElement HealthBarBG;
    VisualElement GameOverLayout;
    VisualElement GameUILayout;
    VisualElement PauseLayout;
    Button PauseButton;
    Button ResumeButton;
    Button PlayButton;
    Button RestartButton;
    Button PauseExitButton;
    Label DifficultyLabel;
    float Health = 100f;
    [SerializeField] float DecayFactor = 1f;
    float BGWidth;
    [SerializeField] bool IsPaused = false;
    private void Start()
    {
        if (GameInitializationParameter.Difficulty == Difficulty.none)
        {
            GameInitializationParameter.Difficulty = Difficulty.Hard;
        }
        if (GameInitializationParameter.Difficulty == Difficulty.Hard) DecayFactor = 1f;
        if (GameInitializationParameter.Difficulty == Difficulty.UltraHard) DecayFactor = 10f;
        VisualElement root = GetComponent<UIDocument>().rootVisualElement;

        #region Game Over Layout
        //init ui objects
        GameOverLayout = root.Q<VisualElement>("GameOverLayout");
        StartButton = GameOverLayout.Q<Button>("RestartButton");
        ExitButton = GameOverLayout.Q<Button>("ExitButton");
        
        StartButton.clicked += ExecuteRestart;
        ExitButton.clicked += ExecuteExit;
        #endregion

        #region Main Game UI
        //init ui objects
        GameUILayout = root.Q<VisualElement>("UI");
        HealthBar = GameUILayout.Q<VisualElement>("HealthBar");
        HealthBarBG = GameUILayout.Q<VisualElement>("HealthBarBG");
        PauseButton = GameUILayout.Q<Button>("PauseButton");
        DifficultyLabel = GameUILayout.Q<Label>("Difficulty");

        //value assignment
        DifficultyLabel.text = Enum.GetName(typeof(Difficulty), GameInitializationParameter.Difficulty);

        //Event handler assignment
        PauseButton.clicked += ExecutePause;
        #endregion


        #region Pause Screen Layout
        //init ui objects
        PauseLayout = root.Q<VisualElement>("PauseLayout");
        PauseExitButton = PauseLayout.Q<Button>("ExitButton");
        RestartButton = PauseLayout.Q<Button>("RestartButton");
        ResumeButton = PauseLayout.Q<Button>("ResumeButton");
        PlayButton = PauseLayout.Q<Button>("PlayButton");
        
        //evend handler assignment
        RestartButton.clicked += ExecuteRestart;
        PauseExitButton.clicked += ExecuteExit;
        ResumeButton.clicked += ExecuteResume;
        PlayButton.clicked += ExecuteResume;
        #endregion

        GameUIVisible();
    }
    private void Update()
    {
        if (IsPaused)
        {
            PauseVisible();
            return;
        }
        BGWidth = HealthBarBG.resolvedStyle.width;
        if (Health <= 0f)
        {
            ExecuteGameOver();
            return;
        }
        Health -= Time.deltaTime * DecayFactor;
        HealthBar.style.width = new(BGWidth - (BGWidth * Health / 100));
    }
    void ExecuteRestart()
    {
        Health = 100f;
        GameUIVisible();
        IsPaused = false;
    }
    void ExecuteExit()
    {
        SceneManagement.LoadSceneWithTransition("StartScene");
    }
    void OnDestroy()
    {
        GameInitializationParameter.Difficulty = Difficulty.none;
    }
    void ExecutePause()
    {
        if (IsPaused) 
        {
            Debug.LogError("Trying to pause the game while its already paused.");
            return;
        }
        PauseVisible();
        IsPaused = true;
    }
    void ExecuteResume()
    {
        if (!IsPaused)
        {
            Debug.LogError("Trying to resume the game while its already in play.");
            return;
        }
        GameUIVisible();
        IsPaused = false;
    }
    void ExecuteGameOver()
    {
        GameOverVisible();
    }

    void GameUIVisible()
    {
        GameUILayout.visible = true;
        PauseLayout.visible = false;
        GameOverLayout.visible = false;
    }
    void GameOverVisible()
    {
        GameUILayout.visible = false;
        PauseLayout.visible = false;
        GameOverLayout.visible = true;
    }
    void PauseVisible()
    {
        GameUILayout.visible = false;
        PauseLayout.visible = true;
        GameOverLayout.visible = false;
    }
}

