using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(BoxCollider2D))]


public class Lur_PlayerMovement2D : MonoBehaviour
{
    [Header("Movimiento")]
    [Min(5), Space(8)]
    public float moveSpeed = 5f; // Velocidad de movimiento
    [Min(16), Space(2)]
    public float jumpForce = 16f; // Fuerza del salto
    [HideInInspector]
    public int maxJumps = 0; // Cantidad m�xima de saltos permitidos

    private Rigidbody2D rb;
    private int jumpsRemaining; // Saltos restantes
    private bool isGrounded; // Si el personaje est� tocando el suelo
    private bool isGrounded2;
    private bool isGrounded3;

    [Header("Chequeo de suelo")]
    public Transform groundCheck; // Objeto para verificar contacto con el suelo
    public Transform groundCheck2; // Objeto para verificar contacto con el suelo
    public Transform groundCheck3; // Objeto para verificar contacto con el suelo
    public float groundCheckDistance = 0f; // Distancia del Raycast para detectar el suelo
    [Tooltip("A�ade un Layer llamado 'Suelo' e indicalo aqu�")]
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

        // Verificar si est� en el suelo usando Raycast
        isGrounded = Physics2D.Raycast(groundCheck.position, Vector2.down, groundCheckDistance, groundLayer);
        isGrounded2 = Physics2D.Raycast(groundCheck2.position, Vector2.down, groundCheckDistance, groundLayer);
        isGrounded3 = Physics2D.Raycast(groundCheck3.position, Vector2.down, groundCheckDistance, groundLayer);

        // Si el personaje est� tocando el suelo, restablece los saltos
        if ((isGrounded) || (isGrounded2) || (isGrounded3))
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
        // Dibuja el Raycast en la escena para ver la detecci�n del suelo
        if ((groundCheck != null) && (groundCheck2 != null) && (groundCheck3 != null))
        {
            Gizmos.color = Color.green;
            Gizmos.DrawLine(groundCheck.position, groundCheck.position + Vector3.down * groundCheckDistance);
            Gizmos.DrawLine(groundCheck2.position, groundCheck2.position + Vector3.down * groundCheckDistance);
            Gizmos.DrawLine(groundCheck3.position, groundCheck3.position + Vector3.down * groundCheckDistance);
        }
    }
}