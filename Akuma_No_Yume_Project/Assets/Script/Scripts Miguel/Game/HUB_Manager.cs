using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HUB_Manager : MonoBehaviour
{
    public static HUB_Manager Instance { get; private set; }
    public Vidas_HUB vidasHUB; //Llamar al panel
    public int PuntosTotales { get; private set; }
    public string sceneName;
    private int vidas = 4;
    private float cooldownCuracion = 5f; // Cooldown de 5 segundos
    private float tiempoUltimaCuracion;

    private bool cercaDeTorre;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Debug.Log("Cuidado! M�s de un GameManager en escena.");
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

    // M�todo actualizado para permitir restar una cantidad espec�fica de vidas
    public void PerderVida(int cantidad = 1)
    {
        vidas -= cantidad;

        if (vidas <= 0)
        {
            vidas = 0;
            // Reiniciamos el nivel.
            SceneManager.LoadScene(sceneName);
        }

        vidasHUB.DesactivarVida(vidas);
        Debug.Log("Vidas restantes: " + vidas);
    }

    private void IntentarCurar()
    {
        // Verificar si est� cerca de una torre.
        if (!cercaDeTorre)
        {
            Debug.Log("No est�s cerca de una torre para curarte.");
            return;
        }

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
            Debug.Log("Curaci�n en cooldown. Espera un poco m�s.");
        }
    }

    public bool RecuperarVida()
    {
        if (vidas == 4)
        {
            Debug.Log("Ya tienes el m�ximo de vidas.");
            return false;
        }

        vidasHUB.ActivarVida(vidas);
        vidas += 1;
        return true;
    }

    public void SetCercaDeTorre(bool estado)
    {
        cercaDeTorre = estado;
    }
}
