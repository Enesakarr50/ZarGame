using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class UIManager : SingletonBehaviour<UIManager>
{
    
    [Header("General")]
    [SerializeField] GameObject MenuPanel;
    [SerializeField] GameObject InGamePanel;
    [SerializeField] GameObject exitToMainMenu;
    [SerializeField] GameObject exitGame;
    

    [Space]
    [SerializeField] TextMeshProUGUI remaingingMoves_txt;
    [SerializeField] TextMeshProUGUI levelNumber_txt;

    [Space]
    [SerializeField] SceneCover sceneCover;

    InputControl inputControl;
    UIState currentState;




    private void Awake() {
        inputControl = new InputControl();
        inputControl.GameControl.Enable();
        inputControl.GameControl.Back.performed += OnPerformBack;

        SwitchState(UIState.Menu);
    }

  
    public void SwitchState(UIState toState){
        switch (toState)
        {
            case UIState.Menu:
            
            InGamePanel.SetActive(false);
            MenuPanel.SetActive(true);
            exitToMainMenu.SetActive(false);
            break;

            case UIState.InGame:
            
            InGamePanel.SetActive(true);
            MenuPanel.SetActive(false);
            break;

            case UIState.Finish:
            
            InGamePanel.SetActive(false);
            MenuPanel.SetActive(false);
            break;
        }

        currentState = toState;
    }

   
    public void ShowCover(string msg, Action onCoverMax){
        sceneCover.gameObject.SetActive(true);
        sceneCover.StartCover(msg, () => onCoverMax.Invoke());
    }

    public void UpdateRemainingMoves(int moves){
        if(moves < 0) remaingingMoves_txt.text = "∞";
        else remaingingMoves_txt.text = moves.ToString();
    }

    public void UpdateLevelNumber(int num){
        levelNumber_txt.text = num.ToString();
    }

    public void ExitToMainMenu(){
        ShowCover("Main Menu", () => SwitchState(UIState.Menu));
    }

   

    private void OnPerformBack(InputAction.CallbackContext c){
        switch (currentState)
        {
            case UIState.Menu:
            exitGame.SetActive(!exitGame.activeInHierarchy);
            break;

            case UIState.InGame:
            exitToMainMenu.SetActive(!exitToMainMenu.activeInHierarchy);
            break;

            case UIState.Finish:
            exitGame.SetActive(!exitGame.activeInHierarchy);
            break;
        }
    }

   
}

public enum UIState{
    Menu,
    InGame,
    Finish
}