using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using System;

public class puerta : MonoBehaviour
{
    public GameObject panel;  // Referencia al panel que se mostrar�
    public float interactionRange = 1f;  // Distancia a la que el jugador puede interactuar con la puerta

    private bool playerInRange = false;  // �Est� el jugador cerca de la puerta?

    void Start()
    {
        if (panel != null)
        {
            panel.SetActive(false);  // Asegurarse de que el panel est� oculto al inicio
        }
    }

    void Update()
    {
        // Si el jugador est� cerca y presiona la tecla para abrir la puerta
        if (playerInRange && Input.GetKeyDown(KeyCode.E))  // Puedes cambiar la tecla "E" por cualquier otra
        {
            TogglePanel();  // Mostrar o ocultar el panel
        }
    }

    // M�todo para activar o desactivar el panel
    void TogglePanel()
    {
        if (panel != null)
        {
            panel.SetActive(!panel.activeSelf);  // Cambiar el estado del panel (abrir/cerrar)
        }
    }

    // Detectar la colisi�n con el jugador (puedes usar un Trigger para no interferir con la f�sica)
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))  // Aseg�rate de que el jugador tenga el tag "Player"
        {
            playerInRange = true;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
       

        if (other.CompareTag("Player"))
        {
            playerInRange = false;
        }
    }
   

}
