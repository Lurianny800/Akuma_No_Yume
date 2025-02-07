using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Lur_PauseMenu : MonoBehaviour
{
    public GameObject pausePanel; // Panel de pausa general
    private Animator pauseAnimator;
    private bool isPaused = false; // Estado de pausa
    private bool isAnimating = false;

    void Start()
    {
        isPaused = false;
        pauseAnimator = pausePanel.GetComponent<Animator>();
        pausePanel.SetActive(false); // Asegurarse de que el panel inicie inactivo
    }
    void Update()
    {
        // Tecla "Esc" para pausar o reanudar el juego
        if (Input.GetKeyDown(KeyCode.Escape) && !isAnimating)
        {
            TogglePause();            
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
        isPaused = true;
        isAnimating = false;
        pausePanel.SetActive(true); // Asegúrate de que el panel esté activo antes de reproducir la animación
        pauseAnimator.Play("PauseSlideIn"); // Reproduce la animación de entrada
        StartCoroutine(DelayPauseTime());
    }

    public void ResumeGame()
    {
        isPaused = false;
        isAnimating = true;
        pauseAnimator.Play("PauseSlideOut"); // Reproduce la animación de salida
        StartCoroutine(HidePausePanelAfterAnimation());
    }
    private IEnumerator DelayPauseTime()
    {
        // Espera el tiempo de la animación antes de pausar el tiempo del juego
        yield return new WaitForSecondsRealtime(0.1f); // Ajusta al tiempo real de la animación
        Time.timeScale = 0f; // Pausa el tiempo del juego
        isAnimating = false;
    }
    private IEnumerator HidePausePanelAfterAnimation()
    {
        // Espera a que la animación de salida termine antes de desactivar el panel
        yield return new WaitForSecondsRealtime(0.3f); // Ajusta al tiempo real de la animación
        pausePanel.SetActive(false);
        Time.timeScale = 1f; // Reanuda el tiempo del juego
        isAnimating = false;
    }    
    public void LoadMainMenu()
    {
        // Cargar la escena del Menú Principal
        Time.timeScale = 1f; // Asegúrate de que el tiempo esté activo al cambiar de escena
        SceneManager.LoadScene("menu principal");
    }
}
