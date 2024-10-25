using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target; // El personaje a seguir
    public float smoothSpeed = 0.125f; // La suavidad del movimiento de la cámara
    public Vector3 offset; // Desplazamiento de la cámara respecto al personaje

    private void LateUpdate()
    {
        if (target != null)
        {
            // Calcula la posición deseada
            Vector3 desiredPosition = target.position + offset;
            // Suaviza el movimiento de la cámara
            Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
            // Asigna la nueva posición a la cámara
            transform.position = new Vector3(smoothedPosition.x, smoothedPosition.y, transform.position.z);
        }
    }

}
