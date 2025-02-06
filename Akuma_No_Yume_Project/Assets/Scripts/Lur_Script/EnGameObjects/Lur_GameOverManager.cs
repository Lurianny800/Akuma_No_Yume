using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Lur_GameOverManager : MonoBehaviour
{
    public static Lur_GameOverManager Instance { get; private set; }
    public GameObject gameOverPanel; // Panel de Game Over
    private string mainMenuScene = "menu principal"; // Nombre de la escena del menú principal

    private bool isGameOver = false;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        gameOverPanel.SetActive(false); // Ocultar el panel al inicio
    }

    public void GameOver()
    {
        if (!isGameOver)
        {
            isGameOver = true;
            gameOverPanel.SetActive(true);
            Time.timeScale = 0f; // Pausar el juego
        }
    }

    public void RestartGame()
    {
        Time.timeScale = 1f; // Reanudar el tiempo
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void GoToMainMenu()
    {
        Time.timeScale = 1f; // Reanudar el tiempo
        SceneManager.LoadScene(mainMenuScene);
    }
}
