using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zamue_camera_follow : MonoBehaviour
{
    public Transform player; // El Transform del jugador
    public Vector2 minLimits; // L�mites m�nimos del mapa (X, Y)
    public Vector2 maxLimits; // L�mites m�ximos del mapa (X, Y)
    public float smoothTime = 0.3f; // Tiempo de suavidad para el movimiento

    private Vector3 velocity = Vector3.zero; // Velocidad del movimiento suavizado

    void LateUpdate()
    {
        if (player == null) return;

        // Obtener la posici�n objetivo de la c�mara basada en el jugador
        Vector3 targetPosition = new Vector3(player.position.x, player.position.y, transform.position.z);

        // Restringir la posici�n dentro de los l�mites del mapa
        targetPosition.x = Mathf.Clamp(targetPosition.x, minLimits.x, maxLimits.x);
        targetPosition.y = Mathf.Clamp(targetPosition.y, minLimits.y, maxLimits.y);

        // Suavizar el movimiento de la c�mara
        transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, smoothTime);
    }
}
