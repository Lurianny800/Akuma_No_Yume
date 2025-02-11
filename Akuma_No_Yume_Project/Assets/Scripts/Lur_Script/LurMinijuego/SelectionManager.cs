using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectionManager : MonoBehaviour
{
    public GameObject[] figuras; // Figuras disponibles en el panel de selección
    public RectTransform panelSeleccion; // Panel donde están las figuras
    private List<GameObject> figurasSeleccionadas = new List<GameObject>(); // Lista de figuras en el centro

    public void SeleccionarFigura(int index)
    {
        if (index < 0 || index >= figuras.Length)
        {
            Debug.LogError("Índice fuera de rango");
            return;
        }

        GameObject figuraSeleccionada = figuras[index];

        // Asegurarnos de que la figura no esté ya seleccionada
        if (figurasSeleccionadas.Contains(figuraSeleccionada)) return;

        // Mover la figura fuera del panel de selección
        figuraSeleccionada.transform.SetParent(panelSeleccion.root, false);

        // Resetear su posición dentro del Canvas (centrado)
        figuraSeleccionada.GetComponent<RectTransform>().anchoredPosition = Vector2.zero;

        // Asegurar que tenga el script Draggable
        if (figuraSeleccionada.GetComponent<Draggable>() == null)
        {
            figuraSeleccionada.AddComponent<Draggable>();
        }

        // Centrar la figura en la cámara
        figuraSeleccionada.GetComponent<Draggable>().CenterFigure(panelSeleccion.GetComponentInParent<Canvas>());

        // Añadir a la lista de figuras seleccionadas
        figurasSeleccionadas.Add(figuraSeleccionada);
    }

    public void RemoverUltimaFigura()
    {
        if (figurasSeleccionadas.Count > 0)
        {
            GameObject ultimaFigura = figurasSeleccionadas[figurasSeleccionadas.Count - 1];
            figurasSeleccionadas.RemoveAt(figurasSeleccionadas.Count - 1);

            // Devolver la figura al panel de selección
            ultimaFigura.transform.SetParent(panelSeleccion, false);

            // Restaurar su posición relativa dentro del panel
            ultimaFigura.GetComponent<RectTransform>().anchoredPosition = Vector2.zero;
        }
    }
}
