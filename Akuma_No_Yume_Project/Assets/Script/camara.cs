using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class camara : MonoBehaviour
{
    public Transform player; // El jugador a seguir
    public float smoothSpeed = 0.125f; // Velocidad de suavizado para el movimiento de la c�mara
    public Vector3 offset; // Desplazamiento relativo entre la c�mara y el jugador

    public float minX, maxX, minY, maxY; // L�mites de movimiento de la c�mara en X y Y

    void LateUpdate()
    {
        // Verificar si el jugador ha sido asignado
        if (player != null)
        {
            // Calcular la posici�n deseada de la c�mara
            Vector3 desiredPosition = player.position + offset;

            // Limitar la posici�n de la c�mara para que no se salga de los l�mites
            desiredPosition.x = Mathf.Clamp(desiredPosition.x, minX, maxX);
            desiredPosition.y = Mathf.Clamp(desiredPosition.y, minY, maxY);

            // Interpolaci�n suave para que la c�mara se mueva suavemente
            Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);

            // Establecer la posici�n final de la c�mara
            transform.position = smoothedPosition;
        }
    }
}
