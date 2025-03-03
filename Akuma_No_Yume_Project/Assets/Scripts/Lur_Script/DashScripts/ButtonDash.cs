using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonDash : MonoBehaviour
{
    public string buttonKey; // Clave única para cada botón (ej. "ButtonA", "ButtonB", "ButtonC")
    private Button button;

    private void Start()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(ActivateButton);

        // Si el botón ya fue presionado antes, lo deshabilitamos
        if (PlayerPrefs.GetInt(buttonKey, 0) == 1)
        {
            button.interactable = false;
        }
    }

    private void ActivateButton()
    {
        PlayerPrefs.SetInt(buttonKey, 1); // Guardamos que se presionó
        PlayerPrefs.Save(); // Guardar en la memoria del juego
        button.interactable = false; // Desactivar el botón después de presionarlo
    }
}
