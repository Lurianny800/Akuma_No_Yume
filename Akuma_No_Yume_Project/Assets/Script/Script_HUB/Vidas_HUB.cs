using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Vidas_HUB : MonoBehaviour
{
    public TextMeshProUGUI puntos;
    public GameObject[] vidas;

    void Update()
    {
        puntos.text = HUB_Manager.Instance.PuntosTotales.ToString();
    }

    public void ActualizarPuntos(int puntosTotales)
    {
        puntos.text = puntosTotales.ToString();
    }

    public void DesactivarVida(int indice)
    {
        vidas[indice].SetActive(false);
    }

    public void ActivarVida(int indice)
    {
        vidas[indice].SetActive(true);
    }
}
