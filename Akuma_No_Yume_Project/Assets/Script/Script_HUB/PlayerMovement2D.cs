using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement2D : MonoBehaviour
{
    [Header("Movimiento")]
    public float moveSpeed = 5f; // Velocidad de movimiento
    public float jumpForce = 10f; // Fuerza del salto
    public int maxJumps = 1; // Cantidad máxima de saltos permitidos

    private Rigidbody2D rb;
    private int jumpsRemaining; // Saltos restantes
    private bool isGrounded; // Si el personaje está tocando el suelo

    [Header("Chequeo de suelo")]
    public Transform groundCheck; // Objeto para verificar contacto con el suelo
    public float groundCheckDistance = 1f; // Distancia del Raycast para detectar el suelo
    public LayerMask groundLayer; // Capa que representa el suelo

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        jumpsRemaining = maxJumps;
    }

    private void Update()
    {
        // Movimiento en el eje X
        float moveInput = Input.GetAxis("Horizontal");
        rb.velocity = new Vector2(moveInput * moveSpeed, rb.velocity.y);

        // Verificar si está en el suelo usando Raycast
        isGrounded = Physics2D.Raycast(groundCheck.position, Vector2.down, groundCheckDistance, groundLayer);

        // Si el personaje está tocando el suelo, restablece los saltos
        if (isGrounded)
        {
            jumpsRemaining = maxJumps;
        }

        // Salto solo si hay saltos disponibles
        if (Input.GetKeyDown(KeyCode.Space) && jumpsRemaining >= 0)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);           
            jumpsRemaining--;
            
        }
    }

    private void OnDrawGizmos()
    {
        // Dibuja el Raycast en la escena para ver la detección del suelo
        if (groundCheck != null)
        {
            Gizmos.color = Color.green;
            Gizmos.DrawLine(groundCheck.position, groundCheck.position + Vector3.down * groundCheckDistance);
        }
    }
}

