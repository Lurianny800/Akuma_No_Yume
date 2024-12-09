using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Corrutina_EnemyPlayer_Lur : MonoBehaviour
{
    [HideInInspector]public int health = 4; // Vida inicial del jugador
    [Range(1,4)]
    public int damage = 1;  // Daño recibido por colisión con un enemigo

    private bool isTouchingEnemy = false; // Condición para la corrutina

    void Start()
    {
        // Inicia la corrutina para esperar el contacto con enemigos
        StartCoroutine(WaitForEnemyContact());
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        // Si el jugador colisiona con un enemigo, activa la condición
        if (collision.gameObject.CompareTag("Enemy"))
        {
            isTouchingEnemy = true;
        }
    }

    IEnumerator WaitForEnemyContact()
    {
        while (health > 0) // Continúa mientras el jugador tenga vida
        {
            // Espera hasta que el jugador toque a un enemigo
            yield return new WaitUntil(() => isTouchingEnemy);

            // Quita vida al jugador
            health -= damage;
            Debug.Log("Vida restante: " + health);

            // Reinicia la condición para esperar el próximo contacto
            isTouchingEnemy = false;

            // Agrega un pequeño tiempo de espera para evitar daño continuo inmediato
            yield return new WaitForSeconds(1.0f);
        }

        Debug.Log("El jugador ha muerto");
    }
}
