using UnityEngine;
using UnityEngine.UI;

public class ScreenModeToggle : MonoBehaviour
{
    public Toggle fullScreenToggle; // Campo público para asignar el Toggle en el Inspector

    private void Start()
    {
        // Si el Toggle no está asignado, búscalo automáticamente
        if (fullScreenToggle == null)
        {
            fullScreenToggle = GetComponent<Toggle>();
            if (fullScreenToggle == null)
            {
                Debug.LogError("No se encontró un componente Toggle. Asegúrate de asignarlo en el Inspector.");
                return;
            }
        }

        // Cargar el estado guardado de pantalla completa (1 = true, 0 = false)
        bool isFullScreen = PlayerPrefs.GetInt("IsFullScreen", 1) == 1;

        // Configurar el estado inicial del Toggle y la pantalla
        fullScreenToggle.isOn = isFullScreen;
        Screen.fullScreen = isFullScreen;

        // Suscribirse al evento de cambio del Toggle
        fullScreenToggle.onValueChanged.AddListener(SetFullScreenMode);
    }

    public void SetFullScreenMode(bool isFullScreen)
    {
        // Cambiar el modo de pantalla completa
        Screen.fullScreen = isFullScreen;

        // Guardar el estado en PlayerPrefs
        PlayerPrefs.SetInt("IsFullScreen", isFullScreen ? 1 : 0);
        PlayerPrefs.Save();

        Debug.Log("Modo de pantalla cambiado a: " + (isFullScreen ? "Pantalla Completa" : "Ventana"));
    }
}
