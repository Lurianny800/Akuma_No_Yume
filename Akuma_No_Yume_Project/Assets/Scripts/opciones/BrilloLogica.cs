using UnityEngine;
using UnityEngine.UI;

public class BrilloLogica : MonoBehaviour
{
    public Slider slider;
    public float sliderValue;
    public Image imageBrightness;

    void Start()
    {
        // Cargar el valor del brillo guardado en PlayerPrefs
        slider.value = PlayerPrefs.GetFloat("brightness", 0.5f);

        // Aplicar el brillo inicial al panel
        UpdateBrightness(slider.value);

        // Asegurarse de que el slider llame al método `ChangeSlider` cuando se modifique
        slider.onValueChanged.AddListener(ChangeSlider);
    }

    public void ChangeSlider(float value)
    {
        sliderValue = value;

        // Guardar el valor del slider en PlayerPrefs
        PlayerPrefs.SetFloat("brightness", sliderValue);

        // Actualizar el brillo del panel
        UpdateBrightness(sliderValue);
    }

    private void UpdateBrightness(float value)
    {
        if (imageBrightness != null)
        {
            imageBrightness.color = new Color(imageBrightness.color.r, imageBrightness.color.g, imageBrightness.color.b, value);
        }
    }
}
