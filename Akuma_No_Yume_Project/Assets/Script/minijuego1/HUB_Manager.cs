using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HUB_Manager : MonoBehaviour
{
   /* public static HUB_Manager Instance { get; private set; }
    public Vidas_HUB vidasHUB; //Llamar al panel
    public int PuntosTotales { get; private set; }

    private int vidas = 5;
    
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Debug.Log("Cuidado! Mas de un GameManager en escena.");
        }
    }
    private void Update()
    {
       
    }

    public void SumarPuntos(int puntosASumar)
    {
        PuntosTotales += puntosASumar;
        vidasHUB.ActualizarPuntos(PuntosTotales);
    }

    public void PerderVida()
    {
        vidas -= 1;

        if (vidas == 0)
        {
            // Reiniciamos el nivel.
            SceneManager.LoadScene(0);
        }

        vidasHUB.DesactivarVida(vidas);
    }*/
   
}
