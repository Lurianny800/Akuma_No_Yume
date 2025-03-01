using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VolumeController : MonoBehaviour
{
    public Slider volumeSlider; // Asegúrate de asignar esto en el Inspector
    private AudioSource[] audioSources; // Almacena los audios dentro de SoundController

    void Start()
    {
        audioSources = GetComponentsInChildren<AudioSource>(); // Obtiene todos los audios dentro del objeto vacío

        if (volumeSlider != null)
        {
            volumeSlider.onValueChanged.AddListener(SetVolume);
            SetVolume(volumeSlider.value); // Ajusta el volumen inicial
        }
    }

    public void SetVolume(float volume)
    {
        foreach (AudioSource audio in audioSources)
        {
            audio.volume = volume;
        }
    }
}

