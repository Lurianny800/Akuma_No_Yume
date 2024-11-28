using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class movimiento2 : MonoBehaviour
{
    [Header("Movimiento y Dash")]
    public float moveSpeed = 5f;         // Velocidad normal de movimiento
    public float dashSpeed = 15f;        // Velocidad al hacer el dash
    public float dashDuration = 0.2f;    // Duraci�n del dash
    private float dashCooldown = 1f;      // Tiempo entre cada dash
    [SerializeField]

    [Header("Salto")]
    public float jumpForce = 10f;        // Fuerza del salto
    private float doubleJumpForce = 6f;   // Fuerza del doble salto
    [SerializeField]
    public LayerMask groundLayer;        // Capa del suelo para comprobar si el jugador est� tocando el suelo

    private float dashTime = 0f;         // Temporizador para el dash
    private float lastDashTime = -1f;    // Temporizador de cooldown del dash
    private Rigidbody2D rb;              // Referencia al Rigidbody2D del jugador
    private bool isDashing = false;      // �El jugador est� en medio del dash?
    private bool isGrounded = false;     // �El jugador est� tocando el suelo?
    private bool canDoubleJump = false;  // �El jugador puede hacer un segundo salto?

    private Vector2 moveDirection;       // Direcci�n del movimiento

    void Start()
    {
        // Obtener el componente Rigidbody2D
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        // Comprobar si el jugador est� tocando el suelo
        isGrounded = Physics2D.OverlapCircle(transform.position - new Vector3(0, 0.1f, 0), 0.1f, groundLayer);

        // Llamar a la funci�n de movimiento
        HandleMovement();

        // Comprobar si el jugador presiona la tecla para hacer dash (Shift)
        if (Input.GetKeyDown(KeyCode.LeftShift) && Time.time >= lastDashTime + dashCooldown)
        {
            StartDash();
        }

        // Comprobar si el jugador presiona la tecla para saltar (Espacio)
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (isGrounded)
            {
                Jump(); // Primer salto
            }
            else if (!isGrounded && canDoubleJump) // Doble salto
            {
                DoubleJump();
            }
        }

        // Actualizar el temporizador de dash
        if (isDashing)
        {
            dashTime -= Time.deltaTime;
            if (dashTime <= 0f)
            {
                StopDash();
            }
        }
    }

    // M�todo que maneja el movimiento del jugador
    void HandleMovement()
    {
        // Obtener la entrada de movimiento (horizontal y vertical)
        float moveX = Input.GetAxisRaw("Horizontal");
        float moveY = Input.GetAxisRaw("Vertical");

        // Asignar la direcci�n del movimiento
        moveDirection = new Vector2(moveX, moveY).normalized;

        // Si no est� en dash, mover al jugador normalmente
        if (!isDashing)
        {
            rb.velocity = new Vector2(moveDirection.x * moveSpeed, rb.velocity.y);  // Mantener la velocidad vertical al caminar
        }
    }

    // Iniciar el dash
    void StartDash()
    {
        isDashing = true;
        dashTime = dashDuration;
        lastDashTime = Time.time;

        // Aumentar la velocidad al dashSpeed (solo en la direcci�n horizontal)
        rb.velocity = new Vector2(moveDirection.x * dashSpeed, rb.velocity.y);
    }

    // Detener el dash
    void StopDash()
    {
        isDashing = false;

        // Despu�s del dash, volver a la velocidad normal
        rb.velocity = new Vector2(moveDirection.x * moveSpeed, rb.velocity.y);
    }

    // M�todo para realizar el salto
    void Jump()
    {
        rb.velocity = new Vector2(rb.velocity.x, jumpForce);  // Aplicar la fuerza de salto en la direcci�n vertical
        canDoubleJump = true;  // El jugador ahora puede hacer un doble salto
    }

    // M�todo para realizar el doble salto
    void DoubleJump()
    {
        rb.velocity = new Vector2(rb.velocity.x, doubleJumpForce);  // Aplicar la fuerza de doble salto
        canDoubleJump = false;  // El jugador ya no puede hacer otro salto hasta que toque el suelo
    }
}

