using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; // Para manejar UI

public class GameManager : MonoBehaviour
{
    public Transform[] goalPositions; // Lista de casillas objetivo
    public LayerMask pushableLayer; // Capa de los objetos empujables
    public GameObject victoryPanel; // Referencia al panel de victoria
    public GameObject restartButton; // Referencia al botón de reinicio

    private bool gameCompleted = false;

    private void Update()
    {
        if (!gameCompleted && CheckWinCondition())
        {
            gameCompleted = true;
            ShowVictoryPanel();
        }
    }

    private bool CheckWinCondition()
    {
        foreach (Transform goal in goalPositions)
        {
            Collider2D hit = Physics2D.OverlapCircle(goal.position, 0.1f, pushableLayer);
            if (hit == null)
            {
                return false; // Si alguna casilla no tiene un objeto, no está completo
            }
        }
        return true; // Todas las casillas están ocupadas
    }

    private void ShowVictoryPanel()
    {
        if (victoryPanel != null)
        {
            victoryPanel.SetActive(true);
        }

        if (restartButton != null)
        {
            restartButton.SetActive(false); // Oculta el botón de reinicio
        }

        Debug.Log("¡Minijuego completado! 🎉");
    }
}