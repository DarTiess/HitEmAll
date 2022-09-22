using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HeathBar : MonoBehaviour
{
    [SerializeField] private Slider slider;
    float valueProgress = 0;
    Camera camera;
   
    private void Awake()
    {
        camera = Camera.main;
    }
    private void Update()
    {
        slider.transform.LookAt(transform.position + camera.transform.forward);
    }

    public void SetOffSlider()
    {
        slider.gameObject.SetActive(false);
    }

    public void SetOnSlider()
    {
        slider.gameObject.SetActive(true);
    }
    public void SetMaxValus(float maxValues)
    {
        slider.maxValue = maxValues;
        slider.value = maxValues;
        valueProgress = maxValues;
    }

    public void SetValues(float price, float time)
    {
        if (valueProgress > 0)
        {
            valueProgress -= price;
            slider.DOValue(valueProgress, time);

        }


    }
}
