using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LurPlayerController : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float forcejump = 5f;
    [Min(1)] // Asegura que `saltosMaximos` sea al menos 1 desde el Inspector
    public int saltosMaximos = 1;
    public LayerMask capaSuelo;

    private Rigidbody2D rb;
    private BoxCollider2D boxCollider;
    private int saltosRestantes;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        boxCollider = GetComponent<BoxCollider2D>();
        saltosRestantes = saltosMaximos; // Inicializa los saltos restantes
    }

    private void Update()
    {
        Movimiento();
        Salto();
    }

    // Comprueba si el personaje está tocando el suelo
    private bool EstaEnSuelo()
    {
        RaycastHit2D raycastHit = Physics2D.BoxCast(boxCollider.bounds.center,
            boxCollider.bounds.size,
            0f,
            Vector2.down,
            0.2f,
            capaSuelo);
        return raycastHit.collider != null;
    }

    // Movimiento horizontal
    private void Movimiento()
    {
        float moveX = Input.GetAxis("Horizontal");
        rb.velocity = new Vector2(moveX * moveSpeed, rb.velocity.y);
    }

    // Control del salto
    private void Salto()
    {
        if (EstaEnSuelo())
        {
            // Restaura los saltos al estar en el suelo
            saltosRestantes = saltosMaximos;
        }

        // Realiza un salto si hay saltos restantes
        if (Input.GetKeyDown(KeyCode.Space) && saltosRestantes >= 0)
        {
            saltosRestantes--; // Reduce los saltos restantes
            rb.velocity = new Vector2(rb.velocity.x, 0f); // Resetea la velocidad vertical para un salto más consistente
            rb.AddForce(Vector2.up * forcejump, ForceMode2D.Impulse);
        }
    }
}
