using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemigo : MonoBehaviour
{
    public Transform player; // El jugador a seguir
    public float detectionRange = 10f; // Rango de detecci�n
    public float attackRange = 1.5f; // Rango de ataque
    public float moveSpeed = 3f; // Velocidad de movimiento del enemigo
    private Rigidbody2D rb; // Componente Rigidbody2D para controlar el movimiento
    private Animator animator; // Animador del enemigo (si lo usas)
    private bool isPlayerGrounded;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>(); // Obtener el componente Rigidbody2D
        animator = GetComponent<Animator>(); // Obtener el componente Animator (si lo usas)
    }

    void Update()
    {

        float distanceToPlayer = Vector2.Distance(transform.position, player.position); // Distancia entre el enemigo y el jugador

        if (distanceToPlayer <= detectionRange) // Si el jugador est� dentro del rango de detecci�n
        {
            ChasePlayer(distanceToPlayer); // El enemigo persigue al jugador

            if (distanceToPlayer <= attackRange) // Si est� dentro del rango de ataque
            {
                AttackPlayer(); // El enemigo ataca al jugador
            }
        }
        else
        {
            Patrol(); // Si no detecta al jugador, patrullar�
        }
    }

    // Funci�n para perseguir al jugador
    void ChasePlayer(float distanceToPlayer)
    {
        Vector2 direction = (player.position - transform.position).normalized; // Direcci�n hacia el jugador
        rb.velocity = new Vector2(direction.x * moveSpeed, direction.y * moveSpeed); // Mover al enemigo hacia el jugador

        if (animator != null)
        {
            animator.SetBool("isWalking", true); // Si usas animaciones
        }


    }

    // Funci�n para atacar al jugador
    void AttackPlayer()
    {
        // Aqu� puedes agregar l�gica de ataque
        if (animator != null)
        {
            animator.SetTrigger("attack"); // Si tienes animaciones de ataque
        }
        Debug.Log("Atacando al jugador");
    }

    // Funci�n para patrullar (puedes a�adir tu propia l�gica de patrullaje aqu�)
    void Patrol()
    {
        rb.velocity = Vector2.zero; // El enemigo se detiene si no est� persiguiendo al jugador

        if (animator != null)
        {
            animator.SetBool("isWalking", false); // Detener animaci�n de caminar
        }
    }
}
