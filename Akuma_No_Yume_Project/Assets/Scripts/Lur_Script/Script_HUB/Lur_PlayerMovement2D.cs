using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(BoxCollider2D))]
public class Lur_PlayerMovement2D : MonoBehaviour
{
    [Header("Movimiento")]
    public float moveSpeed = 5f;
    public float jumpForce = 16f;
    public int maxJumps = 1;

    private Rigidbody2D rb;
    private Animator anim;
    private int jumpsRemaining;
    private bool isGrounded;
    private bool isGrounded2;
    private bool isGrounded3;
    private Vector3 originalScale;
    private bool puedeCurarse = false; // Permite saber si está en la zona de curación

    [Header("Chequeo de suelo")]
    public Transform groundCheck;
    public Transform groundCheck2;
    public Transform groundCheck3;
    public float groundCheckDistance = 0.2f;
    public LayerMask groundLayer;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        jumpsRemaining = maxJumps;
        originalScale = transform.localScale;
    }

    private void Update()
    {
        float moveInput = Input.GetAxis("Horizontal");
        rb.velocity = new Vector2(moveInput * moveSpeed, rb.velocity.y);

        // Animación de caminar
        anim.SetFloat("Speed", Mathf.Abs(moveInput));

        // Voltear el personaje sin cambiar su tamaño
        if (moveInput > 0)
            transform.localScale = new Vector3(originalScale.x, originalScale.y, originalScale.z);
        else if (moveInput < 0)
            transform.localScale = new Vector3(-originalScale.x, originalScale.y, originalScale.z);

        // Verificar si está en el suelo
        isGrounded = Physics2D.Raycast(groundCheck.position, Vector2.down, groundCheckDistance, groundLayer);
        isGrounded2 = Physics2D.Raycast(groundCheck2.position, Vector2.down, groundCheckDistance, groundLayer);
        isGrounded3 = Physics2D.Raycast(groundCheck3.position, Vector2.down, groundCheckDistance, groundLayer);

        bool onGround = isGrounded || isGrounded2 || isGrounded3;

        // Animación de suelo
        anim.SetBool("isGrounded", onGround);

        // Restablecer saltos si está en el suelo
        if (onGround && rb.velocity.y <= 0)
        {
            jumpsRemaining = maxJumps;
            anim.SetBool("isJumping", false);
        }

        // Salto
        if (Input.GetKeyDown(KeyCode.Space) && jumpsRemaining > 0)
        {
            anim.SetBool("isJumping", true);
            anim.SetBool("isGrounded", false);

            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            jumpsRemaining--;
        }

        // Activar animación de curación al presionar "F" dentro del collider
        if (puedeCurarse && Input.GetKeyDown(KeyCode.F))
        {
            anim.SetTrigger("Healing"); // Activa la animación de curación
            StartCoroutine(BloquearMovimiento(2f)); // Bloquea movimiento por 2 segundos
        }
    }

    // Detectar colisión con el objeto "Logro_V1"
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Logros"))
        {
            anim.SetTrigger("PickUp"); // Activa la animación
            rb.velocity = Vector2.zero; // Detiene cualquier movimiento actual
            rb.constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezeRotation; // Congela el movimiento lateral
            enabled = false; // Desactiva este script
            StartCoroutine(ReactivarMovimiento()); // Espera y reactiva el script
        }

        if (other.gameObject.name == "Tower")
        {
            puedeCurarse = true; // Permite curarse cuando está dentro del collider
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.name == "Tower")
        {
            puedeCurarse = false; // Ya no puede curarse al salir del collider
        }
    }

    private IEnumerator ReactivarMovimiento()
    {
        yield return new WaitForSeconds(1.3f);  // Espera a que termine la animación

        rb.constraints = RigidbodyConstraints2D.FreezeRotation; // Descongela el movimiento lateral
        enabled = true; // Reactiva el script
    }

    private IEnumerator BloquearMovimiento(float duracion)
    {
        rb.velocity = Vector2.zero;
        rb.constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezeRotation;
        enabled = false; // Desactiva el script temporalmente

        yield return new WaitForSeconds(duracion); // Espera el tiempo especificado

        rb.constraints = RigidbodyConstraints2D.FreezeRotation; // Reactiva movimiento
        enabled = true; // Reactiva el script
    }

    private void OnDrawGizmos()
    {
        if (groundCheck != null && groundCheck2 != null && groundCheck3 != null)
        {
            Gizmos.color = Color.green;
            Gizmos.DrawLine(groundCheck.position, groundCheck.position + Vector3.down * groundCheckDistance);
            Gizmos.DrawLine(groundCheck2.position, groundCheck2.position + Vector3.down * groundCheckDistance);
            Gizmos.DrawLine(groundCheck3.position, groundCheck3.position + Vector3.down * groundCheckDistance);
        }
    }
}
