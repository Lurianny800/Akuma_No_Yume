using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VolumeSlider : MonoBehaviour
{
    private Slider slider;

    private void Start()
    {
        slider = GetComponent<Slider>();
        slider.value = MusicManager.Instance.GetVolume(); // Inicializa el slider con el volumen actual
        slider.onValueChanged.AddListener(SetVolume);
    }

    private void SetVolume(float value)
    {
        MusicManager.Instance.SetVolume(value); // Ajusta el volumen del AudioSource
    }
}

