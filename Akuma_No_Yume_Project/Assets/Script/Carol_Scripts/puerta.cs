using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using System;

public class puerta : MonoBehaviour
{
    public GameObject panel;  // Referencia al panel que se mostrará
    public float interactionRange = 1f;  // Distancia a la que el jugador puede interactuar con la puerta

    private bool playerInRange = false;  // ¿Está el jugador cerca de la puerta?

    void Start()
    {
        if (panel != null)
        {
            panel.SetActive(false);  // Asegurarse de que el panel esté oculto al inicio
        }
    }

    void Update()
    {
        // Si el jugador está cerca y presiona la tecla para abrir la puerta
        if (playerInRange && Input.GetKeyDown(KeyCode.E))  // Puedes cambiar la tecla "E" por cualquier otra
        {
            TogglePanel();  // Mostrar o ocultar el panel
        }
    }

    // Método para activar o desactivar el panel
    void TogglePanel()
    {
        if (panel != null)
        {
            panel.SetActive(!panel.activeSelf);  // Cambiar el estado del panel (abrir/cerrar)
        }
    }

    // Detectar la colisión con el jugador (puedes usar un Trigger para no interferir con la física)
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))  // Asegúrate de que el jugador tenga el tag "Player"
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
