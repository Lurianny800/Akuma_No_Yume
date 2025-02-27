using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Lur_Fragmento : MonoBehaviour
{
    public GameObject panel; // Asigna el panel desde el Inspector

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player")) // Verifica si el jugador colisiona con el objeto
        {
            panel.SetActive(true); // Activa el panel
            Time.timeScale = 0; // Pausa el juego
            Destroy(gameObject); // Destruye el objeto coleccionable
        }
    }
}
