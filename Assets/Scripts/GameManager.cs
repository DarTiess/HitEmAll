using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public event Action IsStarting;
    public event Action IsGaming;
    public event Action OnFire;
    public event Action OnHit;
    public event Action OnMove;
    public event Action OnPause;
    public event Action OnRestart;
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
    public void Fire()
    {
        OnFire?.Invoke();
    } 
    
    public void Move()
    {
        OnMove?.Invoke();
    }
      public void Hit()
    {
       OnHit?.Invoke();
    }

    public void RestartDisplay()
    {
        OnRestart?.Invoke();
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
    public void RestartScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

}
