using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeSceneOnCollision : MonoBehaviour
{
    public string sceneName; // Nombre de la escena a la que cambiar

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Verificar si el objeto que colisiona es el jugador
        if (other.CompareTag("Player")) // Asegúrate de que tu personaje tenga la etiqueta "Player"
        {
            // Cambiar a la escena especificada
            SceneManager.LoadScene(sceneName);
        }
    }
}
