using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameEvents : MonoBehaviour
{
    public static GameEvents current;
    private void Awake() 
    {
        current = this;    
    }

    public event Action OnInit;
    public void Init()
    {
        Debug.Log("GameEvents::OnInit");
        GameManager.current.State = GameState.Init;
        if (OnInit != null)
        {
            OnInit();
        }
    }

    public event Action OnIdle;
    public void Idle()
    {
        Debug.Log("GameEvents::OnIdle");
        GameManager.current.State = GameState.Idle;
        if (OnIdle != null)
        {
            OnIdle();
        }
    }

    public event Action OnWillMicActivate;
    public void WillMicActivate()
    {
        Debug.Log("GameEvents::OnWillMicActivate");
        GameManager.current.State = GameState.WillMicActivate;
        if (OnWillMicActivate != null)
        {
            OnWillMicActivate();
        }
    }

    public event Action OnDidMicActivate;
    public void DidMicActivate()
    {
        if (OnDidMicActivate != null)
        {
            Debug.Log("GameEvents::OnDidMicActivate");
            GameManager.current.State = GameState.DidMicActivate;
            OnDidMicActivate();
        }
    }

    public event Action<string> OnWillGetWish;
    public void WillGetWish(string wish)
    {
        if (OnWillGetWish != null)
        {
            Debug.Log($"GameEvents::OnWillGetWish:{wish}");
            GameManager.current.State = GameState.WillGetWish;
            OnWillGetWish(wish);
        }
    }

    public event Action<string> OnLoadingWish;
    public void LoadingWish(string wish)
    {
        if (OnLoadingWish != null)
        {
            Debug.Log($"GameEvents::OnLoadingWish:{wish}");
            GameManager.current.State = GameState.LoadingWish;
            OnLoadingWish(wish);
        }
    }

    public event Action<GameObject> OnDidGetWish;
    public void DidGetWish(GameObject obj)
    {
        if (OnDidGetWish != null)
        {
            Debug.Log($"GameEvents::OnDidGetWish:{obj.name}");
            GameManager.current.State = GameState.DidGetWish;
            OnDidGetWish(obj);
        }
    }

    public event Action OnNoResult;
    public void NoResult()
    {
        if (OnNoResult != null)
        {
            Debug.Log("GameEvents::OnNoResult");
            GameManager.current.State = GameState.NoResult;
            OnNoResult();
        }
    }

    public event Action<string> OnLoadError;
    public void LoadError(string error)
    {
        if (OnLoadError != null)
        {
            Debug.Log($"GameEvents::OnLoadError:{error}");
            GameManager.current.State = GameState.LoadError;
            OnLoadError(error);
        }
    }
}
