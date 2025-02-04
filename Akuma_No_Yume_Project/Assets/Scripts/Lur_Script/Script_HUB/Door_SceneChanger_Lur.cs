using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Door_SceneChanger_Lur : MonoBehaviour
{
    public string sceneToLoad; // Nombre de la escena que quieres cargar
    public Transform exitPoint; // Punto donde el jugador aparecerá en la nueva escena
    private bool playerIsNearby = false;

    private static string lastDoorUsed; // Guarda el nombre de la última puerta usada


    private void OnTriggerEnter2D(Collider2D other)
    {
        // Verificar si el objeto que entra en el trigger es el jugador
        if (other.CompareTag("Player"))
        {
            playerIsNearby = true;
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        // Verificar si el jugador ha salido del trigger
        if (other.CompareTag("Player"))
        {
            playerIsNearby = false;
        }
    }
    private void Update()
    {
        // Si el jugador está cerca y presiona la tecla X
        if (playerIsNearby && Input.GetKeyDown(KeyCode.X))
        {
            lastDoorUsed = gameObject.name; // Guarda la última puerta usada            
            SceneManager.LoadScene(sceneToLoad); // Cambiar a la escena especificada
        }
    }
    private void Start()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        GameObject mainCamera = Camera.main.gameObject; // Encuentra la cámara principal

        // Si el jugador viene de otra puerta, lo posicionamos en la correcta
        if (lastDoorUsed == gameObject.name)
        {
            if (player != null && exitPoint != null)
            {
                player.transform.position = exitPoint.position;
            }

            // Mover la cámara a la nueva posición del jugador
            if (mainCamera != null)
            {
                mainCamera.transform.position = new Vector3(exitPoint.position.x, exitPoint.position.y, mainCamera.transform.position.z);
            }
        }
    }
}
