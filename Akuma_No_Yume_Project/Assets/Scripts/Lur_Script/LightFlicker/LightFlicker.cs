using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class LightFlicker : MonoBehaviour
{
    private Light2D light2D; // Referencia a la luz 2D

    [Header("Intensidad de la Luz")]
    public float minIntensity = 0f; // Intensidad mínima
    public float maxIntensity = 1.5f; // Intensidad máxima

    [Header("Velocidad del Parpadeo")]
    public float flickerSpeed = 0.1f; // Tiempo entre parpadeos
    public int flickerCount = 10; // Cantidad de parpadeos antes de la pausa
    public float waitTime = 2f; // Tiempo de espera antes de repetir el ciclo

    private void Start()
    {
        light2D = GetComponent<Light2D>(); // Obtiene la luz 2D
        StartCoroutine(FlickerLoop());
    }

    private IEnumerator FlickerLoop()
    {
        while (true) // Se repite indefinidamente
        {
            for (int i = 0; i < flickerCount; i++) // Hace varios parpadeos
            {
                light2D.intensity = Random.Range(minIntensity, maxIntensity);
                yield return new WaitForSeconds(flickerSpeed);
            }

            // Hace una pausa antes de repetir el parpadeo
            light2D.intensity = maxIntensity; // Mantiene la luz estable durante la pausa
            yield return new WaitForSeconds(waitTime);
        }
    }
}
