using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class Zamue_Cambiar_escena : MonoBehaviour
{
    public string nombreEscena; // El nombre de la escena que quieres cargar

    // Este método se llama cuando otro collider 2D entra en contacto con el collider 2D de este objeto
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Comprobar si el objeto con el que colisionamos tiene el tag "Jugador"
        if (collision.gameObject.CompareTag("Player"))
        {
            // Cargar la escena
            SceneManager.LoadScene(nombreEscena);
        }
    }
}
