using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GameState
{
    None,
    Init,
    Idle,
    WillMicActivate,
    DidMicActivate,
    WillGetWish,
    LoadingWish,
    DidGetWish,
    NoResult,
    LoadError
}

public class GameManager : MonoBehaviour
{
    public static GameManager current;
    public GameState State = GameState.Init;
    private GameState PreviousState = GameState.None;

    [SerializeField]
    private Oculus.Voice.AppVoiceExperience VoiceExperience = null;

    private void Awake() {
        current = this;
    }

    private void Start() {
        GameEvents.current.OnInit += StartGame;

        GameEvents.current.Init();
    }

    private void OnDestroy() {
        GameEvents.current.OnInit -= StartGame;
    }

    private void StartGame()
    {
        Debug.Log($"GameManager::StartGame:{State}");
        GameEvents.current.Idle();
        Debug.Log($"GameManager::StartGame:after:{State}");
    }

    private void Update() {
        if (State != PreviousState)
        {
            // Action only when the state changes
            switch (State)
            {
                case GameState.Init:
                    Debug.Log("GameManager:gameevents::Init");
                    break;
                case GameState.Idle:
                    Debug.Log("GameManager:gameevents::Idle");
                    break;
                case GameState.WillMicActivate:
                    Debug.Log("GameManager:gameevents::WillMicActivate");
                    VoiceExperience.Activate();
                    GameEvents.current.DidMicActivate();
                    break;
                case GameState.DidMicActivate:
                    Debug.Log("GameManager:gameevents::DidMicActivate");
                    break;
                case GameState.WillGetWish:
                    Debug.Log("GameManager:gameevents::WillGetWish");
                    break;
                case GameState.LoadingWish:
                    Debug.Log("GameManager:gameevents::LoadingWish");
                    break;
                case GameState.DidGetWish:
                    Debug.Log("GameManager:gameevents::DidGetWish");
                    break;
                case GameState.NoResult:
                    // handle no result event
                    // then pass to idle state
                    State = GameState.Idle; 
                    break;
                case GameState.LoadError:
                    // handle load error event
                    // then pass to idle state
                    // or try again?
                    State = GameState.Idle;
                    break;
                default:
                    break;
            }

            // Save state
            PreviousState = State;
        }
    }
}

public class WishEventArgs : EventArgs
{
    public string Wish { get; set; }
    public string ModelID { get; set; }
    public string Transcript {get; set; }
}