using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallaxBackground : MonoBehaviour
{
    public Transform player; // Referencia al personaje
    public float parallaxEffect = 0.5f; // Controla la velocidad del movimiento del fondo

    private float length, startpos;
    private Camera cam;

    void Start()
    {
        cam = Camera.main; // Obtiene la cámara principal
        startpos = transform.position.x; // Posición inicial del fondo
        length = GetComponent<SpriteRenderer>().bounds.size.x; // Obtiene el tamaño del fondo (de izquierda a derecha)
    }

    void Update()
    {
        // Calcula el desplazamiento relativo del jugador
        float temp = (cam.transform.position.x * (1 - parallaxEffect));
        float distance = (cam.transform.position.x * parallaxEffect);

        // Desplaza el fondo
        transform.position = new Vector3(startpos + distance, transform.position.y, transform.position.z);

        // Si el fondo ha salido completamente de la vista, lo reposicionamos a su inicio para que se repita
        if (temp > startpos + length)
        {
            startpos += length;
        }
        else if (temp < startpos - length)
        {
            startpos -= length;
        }
    }
}
