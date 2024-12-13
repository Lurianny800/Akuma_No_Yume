using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrightnessApplier : MonoBehaviour
{
    [SerializeField] private Image overlayImage; // Imagen del overlay
    private float brightness = 1f; // Valor inicial predeterminado

    void Start()
    {
        // Obtener el valor inicial desde el controlador global
        if (BrightnessController.Instance != null)
        {
            brightness = BrightnessController.Instance.GetBrightness();
        }

        // Aplicar el brillo inicial
        ApplyBrightness(brightness);
    }

    public void ApplyBrightness(float brightness)
    {
        if (overlayImage != null)
        {
            // Ajustar el color del overlay basado en el brillo
            if (brightness < 1f)
            {
                // Oscurecer (negro con transparencia)
                overlayImage.color = new Color(0f, 0f, 0f, 1f - brightness);
            }
            else
            {
                // Iluminar (blanco con transparencia)
                overlayImage.color = new Color(1f, 1f, 1f, brightness - 1f);
            }
        }
    }

}
