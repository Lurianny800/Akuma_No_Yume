using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonSceneChanger : MonoBehaviour
{
    public string sceneToLoad; // Nombre de la escena a la que se cambiará
    public Vector2 newPosition; // Nueva posición del personaje en la siguiente escena

    public void ChangeScene()
    {
        // Buscar al objeto con el tag "Player"
        GameObject player = GameObject.FindGameObjectWithTag("Player");

        if (player != null)
        {
            // Guardar la nueva posición del Player
            PlayerPrefs.SetFloat("PlayerX", newPosition.x);
            PlayerPrefs.SetFloat("PlayerY", newPosition.y);
            PlayerPrefs.Save(); // Guardar los datos en memoria

            // Cambiar de escena
            SceneManager.LoadScene(sceneToLoad);
            Time.timeScale = 1.0f;
        }
        else
        {
            Debug.LogWarning("No se encontró un objeto con el tag 'Player'.");
        }
    }
}
