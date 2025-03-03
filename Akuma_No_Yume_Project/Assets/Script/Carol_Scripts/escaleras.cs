using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class escaleras : MonoBehaviour
{
    // El nombre de la escena a la que se debe cambiar
    public string sceneToLoad;

    // Este método se llama cuando el jugador entra en el área del Trigger
    private void OnTriggerEnter2D(Collider2D other)
    {
        // Comprobamos si el objeto que tocó el trigger es el jugador
        if (other.CompareTag("Player"))
        {
            // Cargar la nueva escena
            SceneManager.LoadScene(sceneToLoad);
        }
    }
}
