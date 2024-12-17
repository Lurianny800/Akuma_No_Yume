using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Lur_PauseMenu : MonoBehaviour
{
    public GameObject pausePanel; // Panel de pausa general
    public GameObject mapPanel;   // Panel de mapa
    private Animator pauseAnimator; // Animator para controlar la animaci�n
    private bool isAnimating = false; // Evita conflictos durante la animaci�n
    private bool isPaused = false; // Estado de pausa

    void Start()
    {
        isPaused = false;
        pauseAnimator = pausePanel.GetComponent<Animator>(); // Obtener el Animator del panel de pausa
        pausePanel.SetActive(false); // Asegurarse de que el panel inicie inactivo
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

    // M�todo para pausar y reanudar el juego
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
        isAnimating = true; // Bloquea la interacci�n hasta que termine la animaci�n
        pauseAnimator.SetBool("isPaused", true); // Activar animaci�n de entrada
        StartCoroutine(WaitForAnimationToPause());
    }

    public void ResumeGame()
    {
        isAnimating = true; // Bloquea la interacci�n hasta que termine la animaci�n
        pauseAnimator.SetBool("isPaused", false); // Activar animaci�n de salida
        StartCoroutine(WaitForAnimationToResume());
        mapPanel.SetActive(false); // Asegura que el mapa se oculte al reanudar
    }
    private System.Collections.IEnumerator WaitForAnimationToPause()
    {
        // Espera el tiempo de la animaci�n
        yield return new WaitForSecondsRealtime(1f); // Ajusta seg�n la duraci�n de tu animaci�n
        Time.timeScale = 0f; // Congela el tiempo del juego
        isPaused = true;
        isAnimating = false;
    }

    private System.Collections.IEnumerator WaitForAnimationToResume()
    {
        // Espera el tiempo de la animaci�n
        yield return new WaitForSecondsRealtime(1f); // Ajusta seg�n la duraci�n de tu animaci�n
        pausePanel.SetActive(false);
        Time.timeScale = 1f; // Reanuda el tiempo del juego
        isPaused = false;
        isAnimating = false;
    }

    // M�todo para alternar el estado del mapa y pausar el juego
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
        Time.timeScale = 1f; // Aseg�rate de que el tiempo est� activo al cambiar de escena
        SceneManager.LoadScene("Opciones");      
    }
    public void LoadMainMenu()
    {
        // Cargar la escena del Men� Principal
        Time.timeScale = 1f; // Aseg�rate de que el tiempo est� activo al cambiar de escena
        SceneManager.LoadScene("menu principal");
    }
}
