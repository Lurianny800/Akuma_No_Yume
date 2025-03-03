using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DashLocker : MonoBehaviour
{
    public Lur_PlayerMovement2D player; // Referencia al script del personaje que tiene el dash

    void Start()
    {
        if (PlayerPrefs.GetInt("ButtonA", 0) == 1 &&
            PlayerPrefs.GetInt("ButtonB", 0) == 1 &&
            PlayerPrefs.GetInt("ButtonC", 0) == 1)
        {
            player.EnableDash(); // Llamamos a un método para habilitar el dash
        }
    }
}
