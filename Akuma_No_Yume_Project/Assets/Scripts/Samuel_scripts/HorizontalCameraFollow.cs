using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HorizontalCameraFollow : MonoBehaviour
{
    public Transform target; // El personaje a seguir
    public float smoothSpeed = 0.125f; // La suavidad del movimiento de la c�mara
    public Vector3 offset; // Desplazamiento de la c�mara respecto al personaje

    private void LateUpdate()
    {
        if (target != null)
        {
            // Calcula la posici�n deseada solo en el eje X
            Vector3 desiredPosition = new Vector3(target.position.x + offset.x, transform.position.y, transform.position.z);
            // Suaviza el movimiento de la c�mara
            Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
            // Asigna la nueva posici�n a la c�mara
            transform.position = smoothedPosition;
        }
    }
}
