using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class Lur_HealingTower : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Lur_HUBManager.Instance.SetCercaDeTorre(true);
            Debug.Log("Jugador dentro del �rea de curaci�n.");
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Lur_HUBManager.Instance.SetCercaDeTorre(false);
            Debug.Log("Jugador sali� del �rea de curaci�n.");
        }
    }
}
