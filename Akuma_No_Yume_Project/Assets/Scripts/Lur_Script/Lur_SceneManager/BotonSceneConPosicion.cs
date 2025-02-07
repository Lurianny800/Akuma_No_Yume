using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class BotonSceneConPosicion : MonoBehaviour
{
    // Referencia al Transform donde el jugador debería aparecer
    public Transform spawnTransform;

    // Método que cambia de escena y mueve al jugador a la posición del spawnTransform
    public void ChangeScene(string sceneName)
    {
        // Buscar el jugador por tag
        GameObject player = GameObject.FindGameObjectWithTag("Player");

        if (player != null)
        {
            // Mover al jugador a la posición del spawnTransform
            player.transform.position = spawnTransform.position;
            player.transform.rotation = spawnTransform.rotation; // También puedes copiar la rotación si es necesario
        }

        // Cargar la nueva escena
        SceneManager.LoadScene(sceneName);
        Time.timeScale = 1f;
    }
}
