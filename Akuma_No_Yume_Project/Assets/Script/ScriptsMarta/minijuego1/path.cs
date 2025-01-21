using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CompositeCollider2D))]
public class path : MonoBehaviour
{
    public Transform posicionInicial;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            Debug.Log("You fell :(");

            // Obtener el script del jugador
            player playerScript = other.gameObject.GetComponent<player>();
            if (playerScript != null)
            {
                playerScript.EnableInput(false); // Deshabilitar el input
            }

            // Congelar el tiempo
            Time.timeScale = 0;

            // Iniciar la corrutina de respawn y pasar el script del jugador
            StartCoroutine(Respawn(0.5f, other.gameObject, playerScript));
        }
    }

    IEnumerator Respawn(float duration, GameObject player, player playerScript)
    {
        // Esperar en tiempo real (independientemente del Time.timeScale)
        float elapsed = 0;
        while (elapsed < duration)
        {
            elapsed += Time.unscaledDeltaTime;
            yield return null;
        }

        // Reposicionar al jugador
        player.transform.position = posicionInicial.position;

        // Reanudar el tiempo
        Time.timeScale = 1;

        // Reactivar el input
        if (playerScript != null)
        {
            playerScript.EnableInput(true);
        }
    }
}
