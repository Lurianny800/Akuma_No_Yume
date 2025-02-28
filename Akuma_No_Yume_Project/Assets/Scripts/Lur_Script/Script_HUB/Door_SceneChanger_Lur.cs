using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Door_SceneChanger_Lur : MonoBehaviour
{
    public string sceneToLoad; // Nombre de la escena a cargar
    public Vector2 newPlayerPosition; // Nueva posición del jugador después del cambio de escena
    public bool changePlayerPosition; // Si es true, cambia la posición del jugador

    private bool isPlayerInDoor = false; // Verifica si el jugador está en contacto

    private void Update()
    {
        if (isPlayerInDoor && Input.GetKeyDown(KeyCode.X)) // Si el jugador está en la puerta y presiona X
        {
            SceneManager.sceneLoaded += OnSceneLoaded; // Suscribirse al evento de carga
            SceneManager.LoadScene(sceneToLoad); // Cargar la escena
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player")) // Detectar si el objeto en colisión es el jugador
        {
            isPlayerInDoor = true; // Activar la detección
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player")) // Detectar cuando el jugador sale
        {
            isPlayerInDoor = false; // Desactivar la detección
        }
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (changePlayerPosition)
        {
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            if (player != null)
            {
                player.transform.position = newPlayerPosition;
            }
        }
        SceneManager.sceneLoaded -= OnSceneLoaded; // Desuscribirse del evento
    }
}
