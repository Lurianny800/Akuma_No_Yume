using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(SpriteRenderer))]
public class Lur_PlayerAnimator : MonoBehaviour
{
    private Animator anim;
    private SpriteRenderer spriteRenderer;
    private Lur_PlayerMovement2D playerMovement;
    private Rigidbody2D rb;

    private void Start()
    {
        anim = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        playerMovement = GetComponent<Lur_PlayerMovement2D>();
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        // Obtener movimiento en X
        float moveInput = Input.GetAxisRaw("Horizontal");

        // Detectar si está en el suelo
        bool isGrounded = playerMovement.groundCheck != null &&
                          (Physics2D.Raycast(playerMovement.groundCheck.position, Vector2.down, playerMovement.groundCheckDistance, playerMovement.groundLayer) ||
                           Physics2D.Raycast(playerMovement.groundCheck2.position, Vector2.down, playerMovement.groundCheckDistance, playerMovement.groundLayer) ||
                           Physics2D.Raycast(playerMovement.groundCheck3.position, Vector2.down, playerMovement.groundCheckDistance, playerMovement.groundLayer));

        // Actualizar animaciones
        anim.SetBool("isWalking", true);
        anim.SetBool("isJumping", !isGrounded);

        // Voltear sprite si se mueve a la izquierda
        if (moveInput < 0)
            spriteRenderer.flipX = true;
        else if (moveInput > 0)
            spriteRenderer.flipX = false;
    }
}
