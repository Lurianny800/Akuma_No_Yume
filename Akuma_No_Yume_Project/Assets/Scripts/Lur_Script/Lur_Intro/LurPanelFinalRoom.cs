using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LurPanelFinalRoom : MonoBehaviour
{
    public GameObject pausePanel; // Panel de pausa

    private void Start()
    {
        pausePanel.SetActive(false); // Asegurar que el panel esté oculto al inicio
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player")) // Detecta si el jugador entra en el trigger
        {
            PauseGame();
        }
    }

    void PauseGame()
    {
        pausePanel.SetActive(true);
        Time.timeScale = 0; // Pausa el juego
    }

    public void ResumeGame()
    {
        pausePanel.SetActive(false);
        Time.timeScale = 1; // Reanuda el juego
    }

    public void ChangeScene(string sceneName)
    {
        Time.timeScale = 1; // Asegurar que el tiempo se reanude antes de cambiar de escena
        SceneManager.LoadScene(sceneName);
    }
}
