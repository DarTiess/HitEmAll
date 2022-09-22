using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CanvasControl : MonoBehaviour
{
    [SerializeField] private GameObject startingText;
    [SerializeField] private Button restartBtn;
    // Start is called before the first frame update
    void Start()
    {
        GameManager.Instance.IsGaming += StartingGaming;
        GameManager.Instance.OnRestart += RestartingGame;
      
        restartBtn.onClick.AddListener(ClickResratBtn);
        restartBtn.gameObject.SetActive(false);
    }

    

    void StartingGaming()
    {
        startingText.SetActive(false);
    }

    void RestartingGame()
    {
        restartBtn.gameObject.SetActive(true);
    }

    void ClickResratBtn()
    {
        GameManager.Instance.RestartScene();
    }
}
