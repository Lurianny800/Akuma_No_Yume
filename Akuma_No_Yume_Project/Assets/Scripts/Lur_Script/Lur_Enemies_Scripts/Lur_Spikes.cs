using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lur_Spikes : MonoBehaviour
{
    public int damagePerSecond = 1; // Cuántas vidas se restan por segundo
    public float damageInterval = 1f; // Tiempo entre cada daño (1 segundo en este caso)
    private bool isTouchingPlayer = false;
    private Coroutine damageCoroutine;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            isTouchingPlayer = true;
            damageCoroutine = StartCoroutine(DamageOverTime());
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            isTouchingPlayer = false;
            if (damageCoroutine != null)
            {
                StopCoroutine(damageCoroutine);
            }
        }
    }

    private IEnumerator DamageOverTime()
    {
        while (isTouchingPlayer)
        {
            Lur_HUBManager.Instance.PerderVida(); // Llama al sistema de vidas del jugador
            yield return new WaitForSeconds(damageInterval);
        }
    }
}
