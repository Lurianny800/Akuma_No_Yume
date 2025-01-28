using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatingItem : MonoBehaviour
{
    public float floatAmplitude = 0.15f; // Altura máxima del movimiento flotante
    public float floatSpeed = 2f;      // Velocidad del movimiento flotante

    private Vector3 startPos;

    void Start()
    {
        startPos = transform.position; // Guardar la posición inicial
    }

    void Update()
    {
        // Movimiento flotante en el eje Y
        float newY = startPos.y + Mathf.Sin(Time.time * floatSpeed) * floatAmplitude;
        transform.position = new Vector3(startPos.x, newY, startPos.z);
    }
}
