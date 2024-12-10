using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movimiento2D_Miguel : MonoBehaviour
{
    public float velocidad = 5f; // Velocidad de movimiento
    public float fuerzaSalto = 7f; // Fuerza del salto
    public float velocidadDeslizamiento = 10f; // Velocidad del deslizamiento
    public float tiempoDeslizamiento = 0.2f; // Duraci�n del deslizamiento
    public float tiempoEsperaDeslizamiento = 1f; // Tiempo de espera para el siguiente deslizamiento
    private bool enSuelo; // �Est� el jugador en el suelo?
    private bool puedeSaltar; // �El jugador puede hacer un salto?
    private bool deslizandose; // �Est� el jugador desliz�ndose?
    private int saltosRestantes = 1; // N�mero de saltos restantes 

    private Rigidbody2D rb;
    private BoxCollider2D coll;

    private Coroutine corutinaDeslizar; // Coroutine para el deslizamiento
    private Coroutine corutinaEsperaDeslizar; // Coroutine para el tiempo de espera entre deslizamientos

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        coll = GetComponent<BoxCollider2D>();
    }

    void Update()
    {
        // Comprobamos si el jugador est� en el suelo
        enSuelo = Physics2D.IsTouchingLayers(coll, LayerMask.GetMask("Suelo"));

        // Si el jugador est� en el suelo, reiniciamos los saltos
        if (enSuelo)
        {
            saltosRestantes = 1; // Restauramos los saltos disponibles
        }

        // Hacer un movimiento horizontal
        if (!deslizandose)
        {
            float movimiento = Input.GetAxis("Horizontal");
            rb.velocity = new Vector2(movimiento * velocidad, rb.velocity.y);
        }

        // Salto
        if (Input.GetButtonDown("Jump"))
        {
            if (enSuelo || saltosRestantes > 0) // Si est� en el suelo o tiene saltos disponibles
            {
                rb.velocity = new Vector2(rb.velocity.x, fuerzaSalto);
                if (!enSuelo) // Si no estaba en el suelo, decrementamos los saltos
                {
                    saltosRestantes--;
                }
            }
        }

        // Deslizamiento
        if (Input.GetKeyDown(KeyCode.LeftShift) && !deslizandose) // Se activa con la tecla LeftShift
        {
            if (corutinaEsperaDeslizar == null) // Solo se puede deslizar si no hay una corutina de espera activa
            {
                StartCoroutine(IniciarDeslizamiento());
            }
        }
    }

    // Coroutine para iniciar el deslizamiento
    IEnumerator IniciarDeslizamiento()
    {
        deslizandose = true;

        // Direcci�n del deslizamiento seg�n el movimiento del jugador
        float direccion = Mathf.Sign(rb.velocity.x); // La direcci�n ser� la misma que la del movimiento actual
        if (direccion == 0) direccion = 1; // Si no se mueve, el deslizamiento ser� hacia la derecha por defecto

        // Aumentamos la velocidad horizontal para el deslizamiento
        rb.velocity = new Vector2(direccion * velocidadDeslizamiento, rb.velocity.y);

        // Esperamos el tiempo del deslizamiento
        yield return new WaitForSeconds(tiempoDeslizamiento);

        // Terminamos el deslizamiento
        deslizandose = false;
        // Restauramos la velocidad normal del movimiento
        float movimiento = Input.GetAxis("Horizontal");
        rb.velocity = new Vector2(movimiento * velocidad, rb.velocity.y);

        // Esperamos el tiempo de espera para el siguiente deslizamiento
        corutinaEsperaDeslizar = StartCoroutine(EsperarParaDeslizar());
    }

    // Coroutine para el tiempo de espera entre deslizamientos
    IEnumerator EsperarParaDeslizar()
    {
        yield return new WaitForSeconds(tiempoEsperaDeslizamiento);
        corutinaEsperaDeslizar = null; // Terminamos la corutina de espera, permitiendo un nuevo deslizamiento
    }
}
