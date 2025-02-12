using UnityEngine;
using UnityEngine.UI;

public class SelectionManager : MonoBehaviour
{
    public GameObject[] figuras; // Figuras disponibles en el panel de selección
    public RectTransform panelSeleccion; // Panel donde están las figuras
    private GameObject ultimaFiguraSeleccionada; // Referencia a la última figura seleccionada por el usuario
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
        if (index < 0 || index >= figuras.Length)
        {
            Debug.LogError("Índice fuera de rango");
            return;
        }

        GameObject figuraSeleccionada = figuras[index];

        // Asegurarnos de que la figura no esté ya seleccionada
        if (figuraSeleccionada == ultimaFiguraSeleccionada) return;

        // Mover la figura fuera del panel de selección
        figuraSeleccionada.transform.SetParent(panelSeleccion.root, false);

        // Resetear su posición dentro del Canvas (centrado)
        figuraSeleccionada.GetComponent<RectTransform>().anchoredPosition = Vector2.zero;

        // Asegurar que tenga el script Draggable
        if (figuraSeleccionada.GetComponent<Draggable>() == null)
        {
            figuraSeleccionada.AddComponent<Draggable>();
        }

        // Si la figura no ha sido movida, centrarla en la cámara
        if (figuraSeleccionada.transform.parent != panelSeleccion)
        {
            figuraSeleccionada.GetComponent<Draggable>().CenterFigure(panelSeleccion.GetComponentInParent<Canvas>());
        }

        // Registrar la última figura seleccionada
        ultimaFiguraSeleccionada = figuraSeleccionada;
    }

    public void RemoverUltimaFigura()
    {
        if (ultimaFiguraSeleccionada != null)
        {
            // Remover la última figura seleccionada
            ultimaFiguraSeleccionada.transform.SetParent(panelSeleccion, false);

            // Restaurar su posición relativa dentro del panel
            ultimaFiguraSeleccionada.GetComponent<RectTransform>().anchoredPosition = Vector2.zero;

            // Si lo deseas, también podrías eliminar el componente Draggable cuando se remueve
            Destroy(ultimaFiguraSeleccionada.GetComponent<Draggable>());

            // Limpiar la referencia de la última figura seleccionada
            ultimaFiguraSeleccionada = null;
        }
        else
        {
            Debug.LogWarning("No hay figura para remover.");
        }
    }
}
