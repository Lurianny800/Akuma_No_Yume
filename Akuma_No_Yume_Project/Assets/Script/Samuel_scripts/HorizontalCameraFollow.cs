using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HorizontalCameraFollow : MonoBehaviour
{
    public Transform target; // El personaje a seguir
    public float smoothSpeed = 0.125f; // La suavidad del movimiento de la cámara
    public Vector3 offset; // Desplazamiento de la cámara respecto al personaje

    private void LateUpdate()
    {
        if (target != null)
        {
            // Calcula la posición deseada solo en el eje X
            Vector3 desiredPosition = new Vector3(target.position.x + offset.x, transform.position.y, transform.position.z);
            // Suaviza el movimiento de la cámara
            Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
            // Asigna la nueva posición a la cámara
            transform.position = smoothedPosition;
        }
    }
}
