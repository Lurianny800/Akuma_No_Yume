using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class camara : MonoBehaviour
{
    public Transform player; // El jugador a seguir
    public float smoothSpeed = 0.125f; // Velocidad de suavizado para el movimiento de la cámara
    public Vector3 offset; // Desplazamiento relativo entre la cámara y el jugador

    public float minX, maxX, minY, maxY; // Límites de movimiento de la cámara en X y Y

    void LateUpdate()
    {
        // Verificar si el jugador ha sido asignado
        if (player != null)
        {
            // Calcular la posición deseada de la cámara
            Vector3 desiredPosition = player.position + offset;

            // Limitar la posición de la cámara para que no se salga de los límites
            desiredPosition.x = Mathf.Clamp(desiredPosition.x, minX, maxX);
            desiredPosition.y = Mathf.Clamp(desiredPosition.y, minY, maxY);

            // Interpolación suave para que la cámara se mueva suavemente
            Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);

            // Establecer la posición final de la cámara
            transform.position = smoothedPosition;
        }
    }
}
