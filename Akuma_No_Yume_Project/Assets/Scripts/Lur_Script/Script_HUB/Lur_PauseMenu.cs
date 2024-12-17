using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Lur_PauseMenu : MonoBehaviour
{
    public GameObject pausePanel; // Panel de pausa general
    public GameObject mapPanel;   // Panel de mapa
    private Animator pauseAnimator; // Animator para controlar la animación
    private bool isAnimating = false; // Evita conflictos durante la animación
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
        isAnimating = true; // Bloquea la interacción hasta que termine la animación
        pauseAnimator.SetBool("isPaused", true); // Activar animación de entrada
        StartCoroutine(WaitForAnimationToPause());
    }

    public void ResumeGame()
    {
        isAnimating = true; // Bloquea la interacción hasta que termine la animación
        pauseAnimator.SetBool("isPaused", false); // Activar animación de salida
        StartCoroutine(WaitForAnimationToResume());
        mapPanel.SetActive(false); // Asegura que el mapa se oculte al reanudar
    }
    private System.Collections.IEnumerator WaitForAnimationToPause()
    {
        // Espera el tiempo de la animación
        yield return new WaitForSecondsRealtime(1f); // Ajusta según la duración de tu animación
        Time.timeScale = 0f; // Congela el tiempo del juego
        isPaused = true;
        isAnimating = false;
    }

    private System.Collections.IEnumerator WaitForAnimationToResume()
    {
        // Espera el tiempo de la animación
        yield return new WaitForSecondsRealtime(1f); // Ajusta según la duración de tu animación
        pausePanel.SetActive(false);
        Time.timeScale = 1f; // Reanuda el tiempo del juego
        isPaused = false;
        isAnimating = false;
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
