using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mov3 : MonoBehaviour
{
    public float moveSpeed = 5f;  // Velocidad del jugador
    private Rigidbody2D rb;  // Referencia al Rigidbody2D

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();  // Obtén el Rigidbody2D
    }

    private void Update()
    {
        // Captura las entradas de movimiento
        float moveX = Input.GetAxisRaw("Horizontal");  // -1 (izquierda), 1 (derecha)
        float moveY = Input.GetAxisRaw("Vertical");    // -1 (abajo), 1 (arriba)

        // Normaliza para evitar movimiento más rápido en diagonal
        Vector2 movement = new Vector2(moveX, moveY).normalized;

        // Aplica el movimiento al Rigidbody
        rb.velocity = movement * moveSpeed;
    }
}
