using UnityEngine;
using UnityEngine.SceneManagement;

public class MinigameButton : MonoBehaviour
{
    public string nextSceneName; // Nombre de la escena a la que cambiar
    public string minigameKey;   // Nombre único para identificar el minijuego (ej: "WonMinigame1")

    public void OnButtonPressed()
    {
        PlayerPrefs.SetInt(minigameKey, 1); // Guardamos que este minijuego fue completado
        PlayerPrefs.Save(); // Guardamos los datos
        SceneManager.LoadScene(nextSceneName); // Cargamos la siguiente escena
    }
}
