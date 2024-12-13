using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public static BrightnessManager Instance; // Singleton para acceso global

private const string BrightnessKey = "Brightness";
private float brightness = 1.0f; // Valor por defecto

void Awake()
{
    // Configurar el Singleton
    if (Instance == null)
    {
        Instance = this;
        DontDestroyOnLoad(gameObject); // Mantener este objeto entre escenas
    }
    else
    {
        Destroy(gameObject); // Evitar duplicados
        return;
    }

    // Cargar el valor del brillo guardado
    brightness = PlayerPrefs.GetFloat(BrightnessKey, 1.0f);
}

public void SetBrightness(float value)
{
    brightness = value;
    PlayerPrefs.SetFloat(BrightnessKey, brightness); // Guardar el valor
}

public float GetBrightness()
{
    return brightness;
}

