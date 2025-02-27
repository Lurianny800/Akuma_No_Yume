using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class player : MonoBehaviour
{
    [Header("Movement")]
    [Tooltip("Adjust player's movement speed")]
    public float moveSpeed;
    private bool moving;
    private bool inputEnabled = true; // Nueva bandera para habilitar/deshabilitar input
    [HideInInspector] public Vector2 input;

    [Space]
    public healthHUD_Marta vidasHUB; // Llamar al panel

    [Header("Health")]
    [Tooltip("Adjust player's max health")]
    [SerializeField] private int maxHealth = 5;
    [Space]
    [Tooltip("Shows player's current health")]
    [SerializeField] public int currentHealth;

    private Vector2 lastDirection = Vector2.down; // Por defecto, idle mirando hacia abajo

    private buttons panelGameOver;
    private Animator animator;
    private SpriteRenderer spriteRenderer;
    [SerializeField] private Vector2 minBounds; // Límites inferiores (x, y)
    [SerializeField] private Vector2 maxBounds; // Límites superiores (x, y)


    private void Start()
    {
        currentHealth = maxHealth;
        panelGameOver = FindObjectOfType<buttons>();
        animator = GetComponent<Animator>(); // Obtener Animator
        spriteRenderer = GetComponent<SpriteRenderer>(); // Obtener SpriteRenderer
    }

    public void RemoveHealth()
    {
        currentHealth--;
        vidasHUB.DesactivarVida(currentHealth);

        // Si la salud es 0 o menos, notificamos al PauseManager de que el juego terminó
        if (currentHealth <= 0)
        {
            // Llamamos a la función GameOver desde el PauseManager
            if (panelGameOver != null)
            {
                panelGameOver.GameOver();
            }
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Enemy" || other.gameObject.tag == "Path")
        {
            RemoveHealth();
        }
    }

    private void Update()
    {
        // Bloquear input si está deshabilitado
        if (!inputEnabled) return;

        if (!moving)
        {
            input.x = Input.GetAxisRaw("Horizontal");
            input.y = Input.GetAxisRaw("Vertical");

            if (input.x != 0)
            {
                input.y = 0;
            }

            if (input != Vector2.zero)
            {
                lastDirection = input; // Guarda la última dirección de movimiento
                var targetPosition = transform.position;

                targetPosition.x += input.x;
                targetPosition.y += input.y;

                // Limitar el movimiento dentro de los bounds
                targetPosition.x = Mathf.Clamp(targetPosition.x, minBounds.x, maxBounds.x);
                targetPosition.y = Mathf.Clamp(targetPosition.y, minBounds.y, maxBounds.y);

                StartCoroutine(Move(targetPosition));
            }

            // ✅ ACTUALIZAMOS LOS PARÁMETROS DEL ANIMATOR
            animator.SetFloat("MoveX", input.x);
            animator.SetFloat("MoveY", input.y);
            animator.SetBool("IsMoving", input != Vector2.zero);

            // Flip del sprite al moverse a la izquierda
            if (input.x < 0)
            {
                spriteRenderer.flipX = true;
            }
            else if (input.x > 0)
            {
                spriteRenderer.flipX = false;
            }
            // Si el personaje se detiene, activamos la animación de Idle correspondiente
            if (input == Vector2.zero)
            {
                animator.SetFloat("IdleX", lastDirection.x);
                animator.SetFloat("IdleY", lastDirection.y);
            }
        }
    }

    IEnumerator Move(Vector3 targetPosition)
    {
        moving = true;

        while ((targetPosition - transform.position).sqrMagnitude > Mathf.Epsilon)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);
            yield return null;
        }

        transform.position = targetPosition;
        moving = false;
    }

    public void StopMovement()
    {
        StopAllCoroutines(); // Detiene todas las corrutinas activas
        moving = false;      // Reinicia el estado de movimiento
        input = Vector2.zero; // Reinicia el input
        animator.SetBool("IsMoving", false); // Asegurar animación idle al detenerse
    }

    public void EnableInput(bool enable)
    {
        inputEnabled = enable; // Habilita o deshabilita el input
        if (!enable)
        {
            StopMovement(); // Limpia el estado de movimiento si deshabilitamos el input
        }
    }

}
