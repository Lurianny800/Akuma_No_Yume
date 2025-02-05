using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime.Tree;
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
    public Lur_HealingTower torre; //Ref al script
    private int vidas = 4;
    private float cooldownCuracion = 5f; // Cooldown de 5 segundos
    private float tiempoUltimaCuracion;
    private bool cercaDeTorre;


    private void Awake()
    {
        if(Instance == null)
    {
            Instance = this;
        }
    else
        {
            Debug.Log("Cuidado! Más de un GameManager en escena.");
            Destroy(gameObject);
        }

        if (torre == null)
        {
            Debug.LogError("❌ ERROR: La referencia a la torre es NULL en Lur_HUBManager. Asegúrate de asignarla en el Inspector.");
        }
        else
        {
            Debug.Log("✅ Torre asignada correctamente en Lur_HUBManager.");
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
        Debug.Log("Intentando curar...");

        // Buscar todas las torres en la escena con el tag "Torre"
        GameObject[] torres = GameObject.FindGameObjectsWithTag("Torre");

        if (torres.Length == 0) // Si no hay torres en la escena
        {
            Debug.Log("❌ No hay torres en la escena.");
            return;
        }

        // Verificar si ha pasado el cooldown
        if (Time.time - tiempoUltimaCuracion >= cooldownCuracion)
        {
            bool vidaRecuperada = RecuperarVida();
            if (vidaRecuperada)
            {
                tiempoUltimaCuracion = Time.time; // Reinicia el tiempo del cooldown

                foreach (GameObject torreGO in torres)
                {
                    Lur_HealingTower torre = torreGO.GetComponent<Lur_HealingTower>();
                    if (torre != null)
                    {
                        Debug.Log("🔄 Iniciando cooldown en torre: " + torreGO.name);
                        torre.IniciarCooldown(cooldownCuracion);
                    }
                }
            }
        }
        else
        {
            Debug.Log("⏳ Curación en cooldown. Espera un poco más.");
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
