using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class TouchPanel : MonoBehaviour, IPointerClickHandler,  IPointerDownHandler
{
    bool isStarted;
   
   
    public void OnPointerClick(PointerEventData eventData)
    {
        if (!isStarted)
        {
            GameManager.Instance.PlayGame();
            isStarted = true;
           
        }
       
    }

    public void OnPointerDown(PointerEventData eventData)
    {
       PlayerShooting.Instance.CreateBall(); 
       PlayerShooting.Instance.FireBall();
    }

   

  
}
