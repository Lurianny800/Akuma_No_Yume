using System.Collections;
using UnityEngine;

public class Corrutina_EnemyPlayer_Lur : MonoBehaviour
{
    [HideInInspector] public int health = 4; // Vida inicial del jugador
    [Range(1, 4)] public int damage = 1;  // Daño recibido por colisión con un enemigo
    public float knockbackForce = 5f; // Fuerza del retroceso lateral
    public float bounceForce = 3f; // Fuerza del rebote vertical
    public int bounceCount = 3; // Cantidad de rebotes
    public float knockbackDuration = 0.5f; // Duración total del retroceso

    private Rigidbody2D rb;
    private Animator anim;
    private bool isTakingDamage = false;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy") && !isTakingDamage)
        {
            Vector2 knockbackDirection = (transform.position - collision.transform.position).normalized; // Dirección contraria
            anim.SetTrigger("TakeDamage"); // Activa la animación de daño inmediatamente
            Debug.Log("Animación de daño activada"); // Esto te dirá en la consola si se ejecuta
            StartCoroutine(TakeDamage(damage, knockbackDirection));
        }
    }


    IEnumerator TakeDamage(int damageAmount, Vector2 knockbackDir)
    {
        isTakingDamage = true;
        health -= damageAmount;

        anim.SetTrigger("TakeDamage");

        // Deshabilitar movimiento mientras dura la animación
        Lur_PlayerMovement2D playerMovement = GetComponent<Lur_PlayerMovement2D>();
        if (playerMovement != null)
        {
            playerMovement.enabled = false;
        }

        yield return new WaitForSeconds(anim.GetCurrentAnimatorStateInfo(0).length); // Espera a que termine la animación

        if (playerMovement != null)
        {
            playerMovement.enabled = true;
        }

        isTakingDamage = false;
    }

}
