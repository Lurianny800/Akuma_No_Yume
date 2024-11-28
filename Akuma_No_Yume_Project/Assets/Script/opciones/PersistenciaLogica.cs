using UnityEngine;
using UnityEngine.UI;

public class Configuracion : MonoBehaviour
{
    public Slider volumenSlider;
    public Slider brilloSlider;

    // Variables para almacenar las configuraciones
    private float volumen;
    private float brillo;

    void Start()
    {
        // Si no se ha guardado un volumen previamente, se establece un valor por defecto
        volumen = PlayerPrefs.GetFloat("Volumen", 1f); // 1.0f es el valor por defecto
        brillo = PlayerPrefs.GetFloat("Brillo", 1f); // 1.0f es el valor por defecto

        // Aplicar los valores a los sliders
        volumenSlider.value = volumen;
        brilloSlider.value = brillo;

        // Asegurarse de que este objeto no se destruya al cargar una nueva escena
        DontDestroyOnLoad(gameObject);
    }

    void Update()
    {
        // Guardar la configuración cuando cambie el valor del slider
        volumen = volumenSlider.value;
        brillo = brilloSlider.value;

        // Aplicar la configuración de volumen
        AudioListener.volume = volumen;

        // Guardar los valores para que se mantengan entre escenas
        PlayerPrefs.SetFloat("Volumen", volumen);
        PlayerPrefs.SetFloat("Brillo", brillo);
    }
}
