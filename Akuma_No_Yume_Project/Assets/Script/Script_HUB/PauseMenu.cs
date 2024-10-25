using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    public GameObject pausePanel; // El panel de pausa en la UI
    private bool isPaused = false; // Estado de pausa

    void Update()
    {
        // Pausar con la tecla "Esc"
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            TogglePause();
        }
    }

    // Método que se puede usar para pausar y reanudar el juego
    public void TogglePause()
    {
        if (isPaused)
        {
            ResumeGame();
        }
        else
        {
            PauseGame();
        }
    }

    public void PauseGame()
    {
        pausePanel.SetActive(true); // Activa el panel de pausa
        Time.timeScale = 0f;        // Congela el tiempo del juego
        isPaused = true;
    }

    public void ResumeGame()
    {
        pausePanel.SetActive(false); // Desactiva el panel de pausa
        Time.timeScale = 1f;         // Restaura el tiempo normal del juego
        isPaused = false;
    }
}
