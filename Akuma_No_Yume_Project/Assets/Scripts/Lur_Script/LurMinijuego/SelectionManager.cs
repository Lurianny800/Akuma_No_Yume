using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectionManager : MonoBehaviour
{
    public GameObject[] figuras; // Las figuras a seleccionar
    public GameObject figuraSeleccionada; // La figura que se muestra al centro
    private List<GameObject> figurasSeleccionadas = new List<GameObject>(); // Lista de figuras seleccionadas
    public RectTransform panelSeleccion; // Panel de selección donde las figuras se encuentran inicialmente

    // Método para seleccionar una figura
    public void SeleccionarFigura(int index)
    {
        figuraSeleccionada = figuras[index];
        figuraSeleccionada.SetActive(true);

        // Asegurar que tiene Draggable y centrar la figura
        Draggable draggable = figuraSeleccionada.GetComponent<Draggable>();
        if (draggable == null)
        {
            draggable = figuraSeleccionada.AddComponent<Draggable>();
        }
        draggable.CenterFigure(); // ← Llamamos a la función aquí

        // Evita que el Layout Group afecte la figura seleccionada
        figuraSeleccionada.transform.SetParent(null);
        LayoutElement layoutElement = figuraSeleccionada.GetComponent<LayoutElement>();
        if (layoutElement == null)
        {
            layoutElement = figuraSeleccionada.AddComponent<LayoutElement>();
        }
        layoutElement.ignoreLayout = true;

        figurasSeleccionadas.Add(figuraSeleccionada);
    }

    // Método para remover la última figura seleccionada y devolverla al panel
    public void RemoverUltimaFigura()
    {
        if (figurasSeleccionadas.Count > 0)
        {
            GameObject ultimaFigura = figurasSeleccionadas[figurasSeleccionadas.Count - 1];
            figurasSeleccionadas.RemoveAt(figurasSeleccionadas.Count - 1);

            // Devolver la figura al panel de selección
            ultimaFigura.transform.SetParent(panelSeleccion);
            ultimaFigura.transform.localPosition = Vector3.zero; // Resetear su posición dentro del panel

            // Reactivar el Layout Group para que se ordene bien
            LayoutElement layoutElement = ultimaFigura.GetComponent<LayoutElement>();
            if (layoutElement != null)
            {
                layoutElement.ignoreLayout = false;
            }
        }
    }
    public void SelectFigure(GameObject figure)
    {
        // Verifica si la figura tiene el script Draggable
        Draggable draggable = figure.GetComponent<Draggable>();
        if (draggable != null)
        {
            draggable.CenterFigure();
        }
    }
}
