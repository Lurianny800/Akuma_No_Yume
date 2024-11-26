using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class movimiento : MonoBehaviour
{

    public float velocidadMovimiento = 5f;  // Velocidad de movimiento
    public float fuerzaSalto = 7f;          // Fuerza de salto
    public float gravedadMultiplicador = 2f; // Multiplicador de gravedad para un salto más realista

    private float velocidadHorizontal;      // Velocidad horizontal
    private bool estaEnSuelo;               // Si el jugador está tocando el suelo
    private int saltosRestantes = 2;        // Contador de saltos restantes (inicia con 2 para doble salto)

    private Rigidbody2D rb;                 // Referencia al componente Rigidbody2D

    void Start()
    {
        // Obtener el componente Rigidbody2D
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        // Obtener la entrada del jugador
        velocidadHorizontal = Input.GetAxis("Horizontal"); // Movimiento en el eje X (teclas A/D o flechas)

        // Movimiento del jugador
        MoverJugador();

        // Si el jugador está en el suelo o tiene saltos restantes, puede saltar
        if (Input.GetButtonDown("Jump") && (estaEnSuelo || saltosRestantes > 0))
        {
            Saltar();
        }
    }

    void MoverJugador()
    {
        // Movimiento horizontal en el eje X
        Vector2 movimiento = new Vector2(velocidadHorizontal * velocidadMovimiento, rb.velocity.y);

        // Aplicamos el movimiento al Rigidbody2D
        rb.velocity = movimiento;
    }

    void Saltar()
    {
        // Si el jugador está en el suelo, reseteamos los saltos
        if (estaEnSuelo)
        {
            saltosRestantes = 2; // Permitir los dos saltos si está en el suelo
        }

        // Aplicar fuerza de salto en el eje Y
        rb.velocity = new Vector2(rb.velocity.x, 0f); // Restablecer la velocidad Y para evitar acumulación de velocidad
        rb.AddForce(Vector2.up * fuerzaSalto, ForceMode2D.Impulse);

        // Reducir los saltos restantes si el jugador ya no está en el suelo
        saltosRestantes--;
    }

    void FixedUpdate()
    {
        // Aplique gravedad adicional para hacer los saltos más naturales
        if (!estaEnSuelo)
        {
            rb.gravityScale = gravedadMultiplicador; // Aumentamos la gravedad cuando no estamos en el suelo
        }
        else
        {
            rb.gravityScale = 1f; // Restablecemos la gravedad a su valor normal cuando estamos en el suelo
        }
    }

    // Detección cuando el jugador colisiona con el suelo (uso de Collider2D)
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Suelo"))
        {
            estaEnSuelo = true;  // El jugador está tocando el suelo
            saltosRestantes = 2; // Reseteamos los saltos cuando el jugador está en el suelo
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Suelo"))
        {
            estaEnSuelo = false; // El jugador ya no está tocando el suelo
        }
    }
    }

