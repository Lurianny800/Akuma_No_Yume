using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Corrutina_EnemyPlayer_Lur : MonoBehaviour
{
    [HideInInspector]public int health = 4; // Vida inicial del jugador
    [Range(1,4)]
    public int damage = 1;  // Da�o recibido por colisi�n con un enemigo

    private bool isTouchingEnemy = false; // Condici�n para la corrutina

    void Start()
    {
        // Inicia la corrutina para esperar el contacto con enemigos
        StartCoroutine(WaitForEnemyContact());
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        // Si el jugador colisiona con un enemigo, activa la condici�n
        if (collision.gameObject.CompareTag("Enemy"))
        {
            isTouchingEnemy = true;
        }
    }

    IEnumerator WaitForEnemyContact()
    {
        while (health > 0) // Contin�a mientras el jugador tenga vida
        {
            // Espera hasta que el jugador toque a un enemigo
            yield return new WaitUntil(() => isTouchingEnemy);

            // Quita vida al jugador
            health -= damage;
            Debug.Log("Vida restante: " + health);

            // Reinicia la condici�n para esperar el pr�ximo contacto
            isTouchingEnemy = false;

            // Agrega un peque�o tiempo de espera para evitar da�o continuo inmediato
            yield return new WaitForSeconds(1.0f);
        }

        Debug.Log("El jugador ha muerto");
    }
}
