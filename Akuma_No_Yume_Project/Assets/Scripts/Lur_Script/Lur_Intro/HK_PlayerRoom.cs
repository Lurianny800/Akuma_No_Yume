using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HK_PlayerRoom : MonoBehaviour
{
    public float speed = 5f;
    private Vector2 movement;
    private Animator animator;
    private SpriteRenderer spriteRenderer;

    private string lastDirection = "Idle_Front"; // Guarda la última dirección

    void Start()
    {
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        HandleMovement();
        UpdateAnimations();
    }

    void HandleMovement()
    {
        float moveX = Input.GetAxisRaw("Horizontal");
        float moveY = Input.GetAxisRaw("Vertical");

        // Evitar movimiento diagonal
        if (moveX != 0)
        {
            moveY = 0;
        }

        movement = new Vector2(moveX, moveY).normalized;
        transform.position += (Vector3)movement * speed * Time.deltaTime;
    }

    void UpdateAnimations()
    {
        if (movement.x > 0) // Movimiento a la derecha
        {
            animator.Play("Walk_Right");
            spriteRenderer.flipX = false;
            lastDirection = "Idle_Right";
        }
        else if (movement.x < 0) // Movimiento a la izquierda
        {
            animator.Play("Walk_Right"); // Reutilizamos WalkRight con flip
            spriteRenderer.flipX = true;
            lastDirection = "Idle_Right";
        }
        else if (movement.y > 0) // Movimiento hacia arriba
        {
            animator.Play("Walk_Up");
            lastDirection = "Idle_Up";
        }
        else if (movement.y < 0) // Movimiento hacia abajo
        {
            animator.Play("Walk_Front");
            lastDirection = "Idle_front";
        }
        else
        {
            animator.Play(lastDirection); // Mantiene la pose de idle en la última dirección
        }
    }
}
