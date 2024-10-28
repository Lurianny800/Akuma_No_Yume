using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HUB_Manager : MonoBehaviour
{
    public static HUB_Manager Instance { get; private set; }
    public Vidas_HUB vidasHUB; //Llamar al panel
    public int PuntosTotales { get; private set; }

    private int vidas = 4;
    private float cooldownCuracion = 5f; // Cooldown de 5 segundos
    private float tiempoUltimaCuracion;

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
        if (Input.GetKeyDown(KeyCode.F))
        {
            IntentarCurar();
        }
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
    }
    private void IntentarCurar()
    {
        // Verificar si ha pasado el cooldown
        if (Time.time - tiempoUltimaCuracion >= cooldownCuracion)
        {
            bool vidaRecuperada = RecuperarVida();
            if (vidaRecuperada)
            {
                tiempoUltimaCuracion = Time.time; // Reinicia el tiempo del cooldown
            }
        }
        else
        {
            Debug.Log("Curación en cooldown. Espera un poco más.");
        }
    }

    public bool RecuperarVida()
    {
        if (vidas == 4)
        {
            Debug.Log("Ya tienes el máximo de vidas.");
            return false;
        }

        vidasHUB.ActivarVida(vidas);
        vidas += 1;
        return true;
    }
}
