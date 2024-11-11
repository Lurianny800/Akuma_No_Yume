using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    public GameObject pausePanel; // Panel de pausa
    public GameObject mapPanel;   // Panel de mapa

    private Animator pauseAnimator; // Referencia al Animator del panel de pausa
    private bool isPaused = false; // Estado de pausa

    void Start()
    {
        
    }
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
        pauseAnimator = pausePanel.GetComponent<Animator>();
        Debug.Log("PauseGame");
        pausePanel.SetActive(true); // Muestra el panel antes de animarlo
        pauseAnimator.Play("PausePanelSlideIn"); // Reproduce la animación de entrada
        Debug.Log("PlaySlideIn");
        Time.timeScale = 0f;
        isPaused = true;        
    }

    public void ResumeGame()
    {
        pauseAnimator.Play("PausePanelSlideOut"); // Reproduce la animación de salida
        Invoke("HidePausePanel", 1f); // Retrasa el ocultado del panel hasta que termine la animación
        mapPanel.SetActive(false); // Oculta el mapa si estaba abierto
        Debug.Log("PlaySlideOut");
        Time.timeScale = 1f; // Restaura el tiempo normal del juego
        isPaused = false;
    }
    void HidePausePanel()
    {
        pausePanel.SetActive(false); // Oculta el panel tras la animación de salida
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
