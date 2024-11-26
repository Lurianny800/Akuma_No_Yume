using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RespawnOnTouch : MonoBehaviour
{
    // Referencia al objeto de inicio (punto de respawn)
    public Transform respawnPoint;

    // Si el jugador entra en el trigger del objeto
    void OnTriggerEnter2D(Collider2D other)
    {
        // Verifica si el objeto que entra en el trigger tiene la etiqueta "Player"
        if (other.CompareTag("Player"))
        {
            // Mueve al jugador al punto de respawn
            other.transform.position = respawnPoint.position;
        }
    }
}
