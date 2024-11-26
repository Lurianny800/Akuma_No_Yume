using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealingTower : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            HUB_Manager.Instance.SetCercaDeTorre(true);
            Debug.Log("Jugador dentro del �rea de curaci�n.");
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            HUB_Manager.Instance.SetCercaDeTorre(false);
            Debug.Log("Jugador sali� del �rea de curaci�n.");
        }
    }
}
