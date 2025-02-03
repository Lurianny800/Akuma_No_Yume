using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class camaraMOV : MonoBehaviour
{
    public Transform player;

    // Distancia entre la cámara y el jugador en el plano 2D
    public Vector3 offset;

    // Velocidad con la que la cámara sigue al jugador
    public float smoothSpeed = 0.125f;

    void Update()
    {
        // Si no tienes un objeto asignado en el inspector, no hace nada
        if (player == null) return;

        // La posición deseada de la cámara, solo en los ejes X y Y
        Vector3 desiredPosition = new Vector3(player.position.x + offset.x, player.position.y + offset.y, transform.position.z);

        // Movimiento suave de la cámara usando interpolación (smooth)
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);

        // Actualiza la posición de la cámara
        transform.position = smoothedPosition;
    }
}
