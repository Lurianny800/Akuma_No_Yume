using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Lur_SceneTracker : MonoBehaviour
{
    public SceneTrackerData sceneData; // Referencia al ScriptableObject.

    // Cambiar a una escena por índice
    public void ChangeScene(int sceneIndex)
    {
        Time.timeScale = 1;
        sceneData.previousSceneIndex = SceneManager.GetActiveScene().buildIndex; // Registrar la escena actual.
        SceneManager.LoadScene(sceneIndex); // Cambiar a la nueva escena.
    }

    // Volver a la escena anterior
    public void GoToPreviousScene()
    {
        if (sceneData.previousSceneIndex >= 0)
        {
            SceneManager.LoadScene(sceneData.previousSceneIndex);
        }
        else
        {
            Debug.LogWarning("No se ha registrado una escena previa.");
        }
    }
}
