using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class SelectionManager : MonoBehaviour
{
    public GameObject[] figuras; // Figuras disponibles en el panel de selección
    public RectTransform panelSeleccion; // Panel donde están las figuras
    public RectTransform panelDestino; // Panel donde deben centrarse las figuras
    private GameObject ultimaFiguraSeleccionada; // Última figura seleccionada
    public Button botonRemover; // Botón de remover

    void Start()
    {
        // Asegurarse de que el botón de remover esté conectado
        if (botonRemover != null)
        {
            botonRemover.onClick.AddListener(RemoverUltimaFigura);
        }
        else
        {
            Debug.LogError("Botón Remover no asignado en el inspector.");
        }
    }

    public void SeleccionarFigura(int index)
    {

        if(index < 0 || index >= figuras.Length)
        {
            Debug.LogError("Índice fuera de rango");
            return;
        }

        GameObject figuraSeleccionada = figuras[index];

        // 📌 Verificar si la figura está en el `panelSeleccion`
        if (figuraSeleccionada.transform.parent == panelSeleccion)
        {
            // Mover la figura al panelDestino
            figuraSeleccionada.transform.SetParent(panelDestino, false);

            // Asegurarnos de que se actualice la posición en el panel destino
            CenterFigure(figuraSeleccionada);

            // Asegurar que tenga el script Draggable
            Draggable draggable = figuraSeleccionada.GetComponent<Draggable>();
            if (draggable == null)
            {
                draggable = figuraSeleccionada.AddComponent<Draggable>();
            }

            // Informar que se movió correctamente al panel destino
            Debug.Log($"Figura '{figuraSeleccionada.name}' movida correctamente al panel destino.");
        }
        else
        {
            Debug.LogWarning($"La figura '{figuraSeleccionada.name}' ya está en el panel destino o no es hija del panelSeleccion.");
        }

        ultimaFiguraSeleccionada = figuraSeleccionada;
    }
    
    public void RemoverUltimaFigura()
    {
        if (ultimaFiguraSeleccionada != null)
        {
            // Remover la última figura seleccionada
            ultimaFiguraSeleccionada.transform.SetParent(panelSeleccion, false);

            // Restaurar su posición dentro del panel de selección
            ultimaFiguraSeleccionada.GetComponent<RectTransform>().anchoredPosition = Vector2.zero;

            // Eliminar el componente Draggable para que no pueda moverse fuera del panel
            Destroy(ultimaFiguraSeleccionada.GetComponent<Draggable>());

            // Limpiar la referencia de la última figura seleccionada
            ultimaFiguraSeleccionada = null;
        }
        else
        {
            Debug.LogWarning("No hay figura para remover.");
        }
    }
    private void CenterFigure(GameObject figura)
    {
        RectTransform figuraRect = figura.GetComponent<RectTransform>();

        // Asegurar que los anclajes están en el centro
        figuraRect.anchorMin = new Vector2(0.5f, 0.5f);
        figuraRect.anchorMax = new Vector2(0.5f, 0.5f);
        figuraRect.pivot = new Vector2(0.5f, 0.5f);


        // Ajustar la posición dentro del panel
        figuraRect.anchoredPosition = Vector2.zero;

        // Verificar la posición y los anclajes
        Debug.Log($"Figura centrada en: {figuraRect.anchoredPosition}");
        Debug.Log($"Anclajes: {figuraRect.anchorMin} - {figuraRect.anchorMax}");
    }
}
