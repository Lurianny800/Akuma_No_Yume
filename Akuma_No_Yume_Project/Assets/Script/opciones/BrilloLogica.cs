using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class brillo_logica : MonoBehaviour
{

    public Slider slider;
    public float sliderValue;
    public Image imageBrightness;

    // Start is called before the first frame update
    void Start()
    {
        slider.value = PlayerPrefs.GetFloat("brightness", 0.5f);

        imageBrightness.color = new Color(imageBrightness.color.r, imageBrightness.color.g, imageBrightness.color.b, slider.value);
    }

    public void ChangeSlider(float value)
    {
        sliderValue = value;
        PlayerPrefs.SetFloat("brightness", sliderValue);
        imageBrightness.color = new Color(imageBrightness.color.r, imageBrightness.color.g, imageBrightness.color.b, slider.value);

    }

    // Update is called once per frame
    void Update()
    {

    }
}
