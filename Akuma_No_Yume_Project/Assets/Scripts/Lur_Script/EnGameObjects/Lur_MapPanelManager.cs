using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lur_MapPanelManager : MonoBehaviour
{
    [Tooltip("Referencia al panel del mapa")]
    public GameObject mapPanel;

    [Tooltip("Referencia al jugador para teletransportarlo")]
    public Transform player;

    private bool cercaDeTorre = false; // Indica si el jugador está en rango de una torre

    private void Start()
    {
        mapPanel.SetActive(false); // Ocultar el panel al inicio
    }

    private void Update()
    {
        if (cercaDeTorre && Input.GetKeyDown(KeyCode.M))
        {
            ToggleMapPanel();
        }
    }

    private void ToggleMapPanel()
    {
        bool isActive = !mapPanel.activeSelf;
        mapPanel.SetActive(isActive);
        Time.timeScale = isActive ? 0f : 1f; // Pausar el juego cuando el mapa está activo
    }

    public void SetCercaDeTorre(bool estado)
    {
        cercaDeTorre = estado;
    }

    public void TeletransportarJugador(Transform nuevaPosicion)
    {
        player.position = nuevaPosicion.position;
        ToggleMapPanel(); // Cerrar el mapa después de teletransportar
    }
}
