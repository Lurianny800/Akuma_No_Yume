using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Lur_PauseMenu : MonoBehaviour
{
    public GameObject pausePanel; // Panel de pausa general
    public GameObject mapPanel;   // Panel de mapa
    private bool isPaused = false; // Estado de pausa

    void Start()
    {
        isPaused = false;
    }
    void Update()
    {
        // Tecla "Esc" para pausar o reanudar el juego
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

    // Método para pausar y reanudar el juego
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
        Time.timeScale = 0f; // Congela el tiempo del juego
        isPaused = true;
    }

    public void ResumeGame()
    {
        pausePanel.SetActive(false);
        mapPanel.SetActive(false); // Asegura que el mapa se oculte al reanudar
        Time.timeScale = 1f;       // Restaura el tiempo normal del juego
        isPaused = false;
    }

    // Método para alternar el estado del mapa y pausar el juego
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
    public void LoadOptions()
    {
        // Cargar la escena de Opciones
        Time.timeScale = 1f; // Asegúrate de que el tiempo esté activo al cambiar de escena
        SceneManager.LoadScene("Opciones");      
    }
    public void LoadMainMenu()
    {
        // Cargar la escena del Menú Principal
        Time.timeScale = 1f; // Asegúrate de que el tiempo esté activo al cambiar de escena
        SceneManager.LoadScene("menu principal");
    }
}
