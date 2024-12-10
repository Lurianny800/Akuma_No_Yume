using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class ChangeResolution : MonoBehaviour
{
    public Dropdown resolutionDropdown; // Campo p�blico para asignar en el Inspector

    private List<Resolution> customResolutions = new List<Resolution>
    {
        new Resolution { width = 1920, height = 1080 },
        new Resolution { width = 1280, height = 720 },
        new Resolution { width = 960, height = 540 },
        new Resolution { width = 640, height = 480 }
    };

    private void Start()
    {
        // Si el Dropdown no est� asignado, b�scalo autom�ticamente
        if (resolutionDropdown == null)
        {
            resolutionDropdown = GetComponent<Dropdown>();
            if (resolutionDropdown == null)
            {
                Debug.LogError("No se encontr� un componente Dropdown. Aseg�rate de asignarlo en el Inspector.");
                return;
            }
        }

        // Limpia las opciones actuales del Dropdown
        resolutionDropdown.ClearOptions();

        // Genera las opciones de resoluci�n manualmente
        List<string> options = new List<string>();
        int savedResolutionIndex = PlayerPrefs.GetInt("ResolutionIndex", 0); // Cargar �ndice guardado

        for (int i = 0; i < customResolutions.Count; i++)
        {
            string option = customResolutions[i].width + " X " + customResolutions[i].height;
            options.Add(option);
        }

        // Asigna las opciones al Dropdown
        resolutionDropdown.AddOptions(options);

        // Selecciona la resoluci�n guardada o la predeterminada
        resolutionDropdown.value = savedResolutionIndex;
        resolutionDropdown.RefreshShownValue();

        // Cambia la resoluci�n al iniciar el juego
        ChangeScreenResolution(savedResolutionIndex);

        // Suscr�bete al evento de cambio
        resolutionDropdown.onValueChanged.AddListener(ChangeScreenResolution);
    }

    public void ChangeScreenResolution(int index)
    {
        // Cambia la resoluci�n basada en la opci�n seleccionada
        Resolution selectedResolution = customResolutions[index];
        Screen.SetResolution(selectedResolution.width, selectedResolution.height, Screen.fullScreen);
        Debug.Log("Resoluci�n cambiada a: " + selectedResolution.width + "x" + selectedResolution.height);

        // Guarda el �ndice de la resoluci�n seleccionada
        PlayerPrefs.SetInt("ResolutionIndex", index);
        PlayerPrefs.Save();
    }
}


