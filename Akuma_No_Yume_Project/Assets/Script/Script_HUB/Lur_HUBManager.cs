using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Lur_HUBManager : MonoBehaviour
{
    public static Lur_HUBManager Instance { get; private set; }
    [Tooltip("Añade el Panel de Vidas.")]
    public Lur_VidasHUB vidasHUB; //Llamar al panel
    [Tooltip("Escribe el nombre de la escena que se va a recargar cuando pierda.")]
    public string sceneName;
    public int PuntosTotales { get; private set; }

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

            SceneManager.LoadScene(sceneName);
        }

        vidasHUB.DesactivarVida(vidas);
    }
    private void IntentarCurar()
    {
        // Verificar si está cerca de una torre.
        if (!cercaDeTorre)
        {
            Debug.Log("No estás cerca de una torre para curarte.");
            return;
        }
        //Verificar si ha pasado el CoolDown
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

    public void SetCercaDeTorre(bool estado)
    {
        cercaDeTorre = estado;
    }
}
