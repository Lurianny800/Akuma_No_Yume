using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class camaraMOV : MonoBehaviour
{
    public Transform player;

    // Distancia entre la c�mara y el jugador en el plano 2D
    public Vector3 offset;

    // Velocidad con la que la c�mara sigue al jugador
    public float smoothSpeed = 0.125f;

    void Update()
    {
        // Si no tienes un objeto asignado en el inspector, no hace nada
        if (player == null) return;

        // La posici�n deseada de la c�mara, solo en los ejes X y Y
        Vector3 desiredPosition = new Vector3(player.position.x + offset.x, player.position.y + offset.y, transform.position.z);

        // Movimiento suave de la c�mara usando interpolaci�n (smooth)
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);

        // Actualiza la posici�n de la c�mara
        transform.position = smoothedPosition;
    }
}
