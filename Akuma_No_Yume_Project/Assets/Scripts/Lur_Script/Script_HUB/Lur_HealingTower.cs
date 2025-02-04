using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class Lur_HealingTower : MonoBehaviour
{
    private Animator animator;
    private bool enCooldown;
    private void Start()
    {
        animator = GetComponent<Animator>();

        if (animator == null)
        {
            Debug.LogError("ERROR: No se encontró Animator en la torre.");
        }
        else
        {
            Debug.Log("Animator encontrado en la torre.");
        }
    }   

    public void IniciarCooldown(float cooldown)
    {
        if (!enCooldown)
        {
            enCooldown = true;
            animator.SetBool("EnCooldown", true);
            Invoke(nameof(FinCooldown), cooldown);
        }
    }

    private void FinCooldown()
    {
        enCooldown = false;
        animator.SetBool("EnCooldown", false);
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Lur_HUBManager.Instance.SetCercaDeTorre(true);
            Debug.Log("Jugador dentro del área de curación.");
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Lur_HUBManager.Instance.SetCercaDeTorre(false);
            Debug.Log("Jugador salió del área de curación.");
        }
    }
}
