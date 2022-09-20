using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class TouchPanel : MonoBehaviour, IPointerClickHandler, IPointerUpHandler, IPointerDownHandler
{
    bool isStarted;
    [SerializeField] private GameObject startingText;
    public void OnPointerClick(PointerEventData eventData)
    {
        if (!isStarted)
        {
            GameManager.Instance.PlayGame();
            isStarted = true;
            startingText.SetActive(false);
        }
        else
        {
          
          
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        MLSpace.PlayerShootScript.Instance.CreateBall();
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        MLSpace.PlayerShootScript.Instance.FireBall();
    }

  
}
