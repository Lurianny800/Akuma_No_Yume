using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemigo : MonoBehaviour
{
    public Transform player; // El jugador a seguir
    public float detectionRange = 10f; // Rango de detección
    public float attackRange = 1.5f; // Rango de ataque
    public float moveSpeed = 3f; // Velocidad de movimiento del enemigo
    private Rigidbody2D rb; // Componente Rigidbody2D para controlar el movimiento
    private Animator animator; // Animador del enemigo (si lo usas)
    private bool isPlayerGrounded;
    public float vida = 100f;  // Salud del enemigo
    private float siguienteAtaqueTiempo = 0f;
    public float tiempoEntreAtaques = 1f;
    public int danio = 10;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>(); // Obtener el componente Rigidbody2D
        animator = GetComponent<Animator>(); // Obtener el componente Animator (si lo usas)
    }

    void Update()
    {

        float distanceToPlayer = Vector2.Distance(transform.position, player.position); // Distancia entre el enemigo y el jugador

        if (distanceToPlayer <= detectionRange) // Si el jugador está dentro del rango de detección
        {
            ChasePlayer(distanceToPlayer); // El enemigo persigue al jugador

            if (distanceToPlayer <= attackRange) // Si está dentro del rango de ataque
            {
                AttackPlayer(); // El enemigo ataca al jugador
            }
        }
        else
        {
            Patrol(); // Si no detecta al jugador, patrullará
        }
    }

    // Función para perseguir al jugador
    void ChasePlayer(float distanceToPlayer)
    {
        Vector2 direction = (player.position - transform.position).normalized; // Dirección hacia el jugador
        rb.velocity = new Vector2(direction.x * moveSpeed, direction.y * moveSpeed); // Mover al enemigo hacia el jugador

        if (animator != null)
        {
            animator.SetBool("isWalking", true); // Si usas animaciones
        }


    }

    // Función para atacar al jugador
    void AttackPlayer()
    {
        // Aquí puedes agregar lógica de ataque
        if (animator != null)
        {
            animator.SetTrigger("attack"); // Si tienes animaciones de ataque
        }
        Debug.Log("Atacando al jugador");
    }
    void OnCollisionEnter2D(Collision2D col)
    { 
     if (col.gameObject.CompareTag("Player")) // Asegúrate de que el jugador tenga la etiqueta "Jugador"
        {
            // Solo hace daño si el tiempo lo permite
            if (Time.time >= siguienteAtaqueTiempo)
            {
                // Obtiene el componente JugadorVida del jugador y le aplica daño
                vida jugadorVida = col.gameObject.GetComponent<vida>();
                if (jugadorVida != null)
                {
                    jugadorVida.RecibirDanio(danio);  // Aplica el daño al jugador
                    siguienteAtaqueTiempo = Time.time + tiempoEntreAtaques;  // Restablece el tiempo de ataque
                }
            }
        }
}

    // Función para patrullar (puedes añadir tu propia lógica de patrullaje aquí)
    void Patrol()
    {
        rb.velocity = Vector2.zero; // El enemigo se detiene si no está persiguiendo al jugador

        if (animator != null)
        {
            animator.SetBool("isWalking", false); // Detener animación de caminar
        }
    }
    // Método que se llama cuando el enemigo recibe daño
    public void TakeDamage(float damageAmount)
    {
        vida -= damageAmount;
        Debug.Log("Enemy Health: " +vida);

        if (vida <= 0f)
        {
            Die();  // Si la salud llega a 0, el enemigo muere
        }
    }

    // Método para manejar la muerte del enemigo
    void Die()
    {
        Debug.Log("Enemy died");
        Destroy(gameObject);  // Destruir el objeto enemigo
    }
}
