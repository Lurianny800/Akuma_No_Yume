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
        // Guardar el nombre de la escena actual antes de cambiar
        PlayerPrefs.SetString("PreviousScene", SceneManager.GetActiveScene().name);

        // Guardar la nueva posición SOLO si estamos cambiando de Scena B a Scena A
        if (sceneToLoad == "LVL_0" && SceneManager.GetActiveScene().name == "game1")
        {
            PlayerPrefs.SetFloat("PlayerX", newPosition.x);
            PlayerPrefs.SetFloat("PlayerY", newPosition.y);
        }

        PlayerPrefs.Save(); // Guardar los datos

        // Cambiar de escena
        SceneManager.LoadScene(sceneToLoad);
        Time.timeScale = 1.0f;
    }
}
