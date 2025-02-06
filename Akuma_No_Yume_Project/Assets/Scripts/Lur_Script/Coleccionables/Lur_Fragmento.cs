using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Lur_Fragmento : MonoBehaviour
{
    [Tooltip("Nombre de la escena a la que se cambiará.")]
    public string sceneToLoad;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player")) // Verifica si el jugador entra en contacto
        {
            SceneManager.LoadScene(sceneToLoad); // Cambia a la nueva escena
        }
    }
}
