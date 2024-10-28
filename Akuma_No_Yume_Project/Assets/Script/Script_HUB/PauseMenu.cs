using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    public GameObject pausePanel; // Panel de pausa
    public GameObject mapPanel;   // Panel de mapa
    private bool isPaused = false; // Estado de pausa

    void Update()
    {
        // Tecla "Esc" para pausar o reanudar
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            TogglePause();
        }

        // Tecla "M" para abrir o cerrar el mapa
        if (Input.GetKeyDown(KeyCode.M))
        {
            ToggleMap();
        }
    }

    // Método para pausar y reanudar
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
        pausePanel.SetActive(true);
        Time.timeScale = 0f;
        isPaused = true;
    }

    public void ResumeGame()
    {
        pausePanel.SetActive(false);
        mapPanel.SetActive(false); // Oculta el mapa si estaba abierto
        Time.timeScale = 1f; // Restaura el tiempo normal del juego
        isPaused = false;
    }

    // Método para abrir el mapa y pausar
    public void ToggleMap()
    {
        if (mapPanel.activeSelf)
        {
            mapPanel.SetActive(false); // Oculta el mapa
            Time.timeScale = 1f;       // Reanuda el juego
            isPaused = false;
        }
        else
        {
            mapPanel.SetActive(true);  // Muestra el mapa
            Time.timeScale = 0f;       // Pausa el juego
            isPaused = true;
        }
    }
}
