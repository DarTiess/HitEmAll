using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class TouchPanel : MonoBehaviour, IPointerClickHandler
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
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
