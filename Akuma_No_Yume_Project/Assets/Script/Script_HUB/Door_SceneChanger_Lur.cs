using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Door_SceneChanger_Lur : MonoBehaviour
{
    public string sceneToLoad; // Nombre de la escena que quieres cargar
    private bool playerIsNearby = false;

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
            // Cambiar a la escena especificada
            SceneManager.LoadScene(sceneToLoad);
        }
    }
}
