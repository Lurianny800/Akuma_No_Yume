using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coleccionable_Lur : MonoBehaviour
{
    public int points = 1; // Puntos que este coleccionable otorgar�

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Verificar si el objeto que entra en el trigger es el jugador
        if (collision.CompareTag("Player"))
        {
            // Sumar puntos al sistema (puedes a�adir tu l�gica aqu�)
            Debug.Log("Coleccionado! Se sumaron " + points + " puntos.");

            // Desactivar o destruir el objeto
            Destroy(gameObject);
        }
        if (collision.CompareTag("Player"))
        {
            // Sumar puntos al GameManager
            GameManager_Lur.instance.AddScore(points);

            // Destruir el coleccionable
            Destroy(gameObject);
        }
    }           
}
