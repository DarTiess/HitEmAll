using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public event Action IsStarting;
    public event Action IsGaming;
    public event Action OnPause;
    public event Action IsFail;
    public event Action IsWin;
    public static GameManager Instance { get; private set; }
    private void Awake()
    {
        Instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        StartGame();
    }

    // Update is called once per frame

    public void StartGame()
    {
      //  Time.timeScale = 0;
        IsStarting?.Invoke();
    }

    public void PlayGame()
    {
        Time.timeScale = 1;
        IsGaming?.Invoke();
    }

    public void FailGame()
    {
        IsFail?.Invoke();
        Time.timeScale = 0;

    }
    public void WinGame()
    {
        IsWin?.Invoke();
        Time.timeScale = 0;
    }
    public void PauseGame()
    {
        OnPause?.Invoke();
        Time.timeScale = 0;
    }
  

    public void QuiteGame()
    {
        Application.Quit();
    }


}
