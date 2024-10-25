using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VolumenLogica : MonoBehaviour
{

    public Slider slider;
    public float sliderValue;

    // Start is called before the first frame update
    void Start()
    {
        slider.value = PlayerPrefs.GetFloat("audioVol", 0.5f);
        AudioListener.volume = slider.value;
    }

    public void ChangeSlider(float value)
    {
        sliderValue = value;
        PlayerPrefs.SetFloat("audioVol", sliderValue);
        AudioListener.volume = sliderValue;
    }

    // Update is called once per frame
    void Update()
    {

    }
}