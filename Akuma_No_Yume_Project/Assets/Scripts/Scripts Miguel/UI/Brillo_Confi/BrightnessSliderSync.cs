using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BrightnessSliderSync : MonoBehaviour
{
    private Slider slider;

    void Start()
    {
        slider = GetComponent<Slider>();

        if (BrightnessController.Instance != null)
        {
            // Sincronizar el Slider con el valor inicial del brillo
            slider.value = BrightnessController.Instance.GetBrightness();

            // Conectar el evento para que actualice el brillo en tiempo real
            slider.onValueChanged.AddListener(OnSliderValueChanged);
        }
    }

    void OnSliderValueChanged(float value)
    {
        if (BrightnessController.Instance != null)
        {
            // Actualizar el valor de brillo en el controlador
            BrightnessController.Instance.SetBrightness(value);

            // Aplicar el brillo en tiempo real
            BrightnessApplier applier = FindObjectOfType<BrightnessApplier>();
            if (applier != null)
            {
                applier.ApplyBrightness(value);
            }
        }
    }
    void OnDestroy()
    {
        if (slider != null)
        {
            slider.onValueChanged.RemoveListener(OnSliderValueChanged);
        }
    }


}
