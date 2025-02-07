using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Lur_RecuerdoItem : MonoBehaviour
{
    public GameObject recuerdoPanel;  // El panel a mostrar
    public float tiempoAparecer = 2f;  // Tiempo antes de mostrar el panel
    public float tiempoVisible = 5f;   // Tiempo que el panel permanece visible

    private void Start()
    {
        if (recuerdoPanel != null)
        {
            recuerdoPanel.SetActive(false);  // Asegúrate de que el panel esté desactivado inicialmente
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            StartCoroutine(MostrarRecuerdo());
            Destroy(gameObject);  // Destruir el ítem después de recogerlo
        }
    }

    private IEnumerator MostrarRecuerdo()
    {
        // Espera el tiempo antes de mostrar el panel
        yield return new WaitForSeconds(tiempoAparecer);

        // Muestra el panel
        recuerdoPanel.SetActive(true);

        // Espera el tiempo que el panel debe estar visible
        yield return new WaitForSeconds(tiempoVisible);

        // Desactiva el panel
        recuerdoPanel.SetActive(false);
    }
}
