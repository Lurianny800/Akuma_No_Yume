using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundScroller : MonoBehaviour
{
    public float scrollSpeed = 0.1f; // Velocidad del desplazamiento de la textura
    private Renderer rend;

    void Start()
    {
        rend = GetComponent<Renderer>(); // Obtiene el Renderer del objeto (en este caso, el Sprite Renderer)
    }

    void Update()
    {
        // Desplaza la textura en función del tiempo y la velocidad
        float offset = Time.time * scrollSpeed;

        // Cambia el offset de la textura (mueve la imagen)
        rend.material.SetTextureOffset("_MainTex", new Vector2(offset, 0));
    }
}
