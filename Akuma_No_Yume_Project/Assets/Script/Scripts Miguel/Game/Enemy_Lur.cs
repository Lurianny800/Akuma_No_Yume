using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Lur : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.name == "Player_Hikari") // Detecta si colisiona con el jugador
        {
            // Verifica si este enemigo es "Suelo_Mortal"
            if (gameObject.name == "Suelo_Mortal")
            {
                HUB_Manager.Instance.PerderVida(4); // Quita 4 vidas si es "Suelo_Mortal"
            }
            else
            {
                HUB_Manager.Instance.PerderVida(1); // Quita 1 vida para otros enemigos
            }
        }
    }
}
