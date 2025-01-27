using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class buttons : MonoBehaviour
{
    public GameObject pausePanel; // Panel de pausa en la escena
    private bool isPaused = false; // Estado del juego

    public GameObject gameOverPanel; // Panel de Game Over
    public GameObject player; // Referencia al objeto del jugador (Player)
    private player playerScript; // Referencia al script del jugador

    void Start()
    {
        playerScript = player.GetComponent<player>(); // Obtener el script del jugador
    }
    void Update()
    {
        // Detectar si se presiona la tecla ESC
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused)
            {
                ResumeGame();
            }
            else
            {
                PauseGame();
            }

            // Verificar si las vidas del jugador llegaron a 0
            if (playerScript != null && playerScript.currentHealth <= 0)
            {
                GameOver(); // Llamar a GameOver si las vidas llegaron a 0
            }
        }
    }

    public void GameOver()
    {
        // Solo activar el panel si no está ya activo
        if (gameOverPanel != null && !gameOverPanel.activeSelf)
        {
            gameOverPanel.SetActive(true); // Activar el panel de Game Over
            Time.timeScale = 0f; // Congelar el tiempo
            isPaused = true; // Asegurarse de que el juego está pausado
        }
    }

    public void PauseGame()
    {
        // Activar el panel de pausa y detener el juego
        pausePanel.SetActive(true);
        Time.timeScale = 0f; // Congelar el tiempo
        isPaused = true;
    }

    public void ResumeGame()
    {
        // Desactivar el panel de pausa y reanudar el juego
        pausePanel.SetActive(false);
        Time.timeScale = 1f; // Reanudar el tiempo
        isPaused = false;
    }

    public void AbandonMinigame()
    {
        Debug.Log("Volver al nivel metroidvania");
    }

    public void ReturnToMenu()
    {
        Debug.Log("Volver al menú principal");
    }

    // Función para reiniciar la escena
    public void RestartScene()
    {
        Time.timeScale = 1f; // Asegúrate de reanudar el tiempo al reiniciar
        SceneManager.LoadScene(SceneManager.GetActiveScene().name); // Carga la escena actual
    }

}
