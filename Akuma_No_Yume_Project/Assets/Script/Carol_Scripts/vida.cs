using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class vida : MonoBehaviour
{
    [Header("Vida")]
    public int vidaMaxima = 100;      // Vida máxima del jugador
    public int vidaActual;

    [Header("barra de vida")]
    public Slider barraDeVida;// Vida actual del jugador

    public delegate void OnMuerte();
    public event OnMuerte eventoMuerte;
    // Start is called before the first frame update
    void Start()
    {
        // Inicia la vida actual con la vida máxima
        vidaActual = vidaMaxima;

        // Si tienes una barra de vida, actualiza su valor
        if (barraDeVida != null)
        {
            barraDeVida.maxValue = vidaMaxima;
            barraDeVida.value = vidaActual;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (barraDeVida != null)
        {
            barraDeVida.value = vidaActual;
        }
    }
    public void RecibirDanio(int danio)
    {
        vidaActual -= danio;

        // Asegurarse de que la vida no sea negativa
        if (vidaActual < 0)
        {
            vidaActual = 0;
        }

        // Si la vida llega a 0, el jugador muere
        if (vidaActual == 0)
        {
            Muerte();
        }
    }
    public void Curarse(int cantidad)
    {
        vidaActual += cantidad;

        // Asegurarse de que no sobrepase la vida máxima
        if (vidaActual > vidaMaxima)
        {
            vidaActual = vidaMaxima;
        }
    }
    private void Muerte()
    {
        // Aquí puedes poner la lógica de muerte (por ejemplo, reiniciar el nivel, mostrar un mensaje, etc.)
        Debug.Log("El jugador ha muerto.");

        // Si hay algún evento de muerte, lo invoca
        if (eventoMuerte != null)
        {
            eventoMuerte.Invoke();
        }

        // Opcional: Reiniciar el nivel o desactivar el jugador
        // SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        // gameObject.SetActive(false);
    }
}
