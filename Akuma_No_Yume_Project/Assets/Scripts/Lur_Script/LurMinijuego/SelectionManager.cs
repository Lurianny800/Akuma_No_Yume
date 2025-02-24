using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SelectionManager : MonoBehaviour
{
    // ---------- [Fase de Selección de Figuras] ----------
    [Header("Configuración de las Figuras")]
    [Tooltip("Figuras disponibles en el panel de selección.")]
    public GameObject[] figuras; // Figuras disponibles en el panel de selección

    [Tooltip("Panel donde están las figuras para seleccionarlas.")]
    public RectTransform panelSeleccion; // Panel donde están las figuras

    [Tooltip("Panel donde deben centrarse las figuras al ser seleccionadas.")]
    public RectTransform panelDestino; // Panel donde deben centrarse las figuras

    private GameObject ultimaFiguraSeleccionada; // Última figura seleccionada

    // ---------- [Botones] ----------
    [Header("Botones de Interacción")]
    [Tooltip("Botón para remover la última figura seleccionada.")]
    public Button botonRemover; // Botón de remover

    [Tooltip("Botón para subir la capa de la figura.")]
    public Button botonSubirCapa; // Botón para subir la figura de capa

    [Tooltip("Botón para bajar la capa de la figura.")]
    public Button botonBajarCapa; // Botón para bajar la figura de capa

    // ---------- [Configuración de Victoria] ----------
    [Header("Condiciones de Victoria")]
    [Tooltip("Texto que aparecerá cuando el jugador complete el minijuego.")]
    public TextMeshProUGUI textoCompletado; // Texto de completado usando TMP

    [Tooltip("Posiciones correctas para cada figura, en el orden de selección.")]
    public Vector2[] posicionesCorrectas; // Posiciones correctas para cada figura

    [Tooltip("Orden de las capas (sortingOrder) correcto para cada figura.")]
    public int[] sortingOrderCorrectos; // Orden en la capa correcto para cada figura

    //Variables privadas.
    // Límites para el sortingOrder
    private int defaultSortingOrder; // Almacenar el valor de sortingOrder original
    private int minSortingOrder = 1;
    private int maxSortingOrder = 3;
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
        // Asignar los botones de subir y bajar capa
        if (botonSubirCapa != null)
        {
            botonSubirCapa.onClick.AddListener(SubirCapaFigura);
        }
        if (botonBajarCapa != null)
        {
            botonBajarCapa.onClick.AddListener(BajarCapaFigura);
        }
        // Desactivar el texto de completado al inicio
        if (textoCompletado != null)
        {
            textoCompletado.gameObject.SetActive(false);
        }
    }
    void Update()
    {
        // Comprobar si el juego ha terminado
        if (VerificarCondicionesDeVictoria())
        {
            MostrarCompletado();
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
            Draggable draggable = figuraSeleccionada.GetComponent<Draggable>();
            if (draggable != null)
            {
                draggable.MoveToPanel(panelDestino);
            }
            // Al seleccionar la figura, almacenamos su sortingOrder original
            SpriteRenderer spriteRenderer = figuraSeleccionada.GetComponent<SpriteRenderer>();
            if (spriteRenderer != null)
            {
                defaultSortingOrder = spriteRenderer.sortingOrder; // Obtener el sortingOrder
            }
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

            // Restaurar el sortingOrder original usando SpriteRenderer
            SpriteRenderer spriteRenderer = ultimaFiguraSeleccionada.GetComponent<SpriteRenderer>();
            if (spriteRenderer != null)
            {
                spriteRenderer.sortingOrder = defaultSortingOrder;
            }

            // Desactivar Draggable en lugar de eliminarlo
            Draggable draggable = ultimaFiguraSeleccionada.GetComponent<Draggable>();
            if (draggable != null)
            {
                draggable.MoveToPanel(panelSeleccion);
            }

            // Limpiar la referencia de la última figura seleccionada
            ultimaFiguraSeleccionada = null;
        }
        else
        {
            Debug.LogWarning("No hay figura para remover.");
        }
    }
    // Método para verificar si todas las figuras están correctamente colocadas
    private bool VerificarCondicionesDeVictoria()
    {
        for (int i = 0; i < figuras.Length; i++)
        {
            GameObject figura = figuras[i];

            // Comprobar si cada figura está en el panelDestino
            if (figura.transform.parent != panelDestino)
            {
                return false; // Si alguna no está en el PanelDestino, el juego no está completo
            }

            // Comprobar si la figura tiene la posición correcta
            RectTransform rectTransform = figura.GetComponent<RectTransform>();
            if (rectTransform != null)
            {
                if (rectTransform.anchoredPosition != posicionesCorrectas[i])
                {
                    return false; // Si la posición no es correcta, el juego no está completo
                }
            }

            // Comprobar si la figura tiene el sortingOrder correcto
            SpriteRenderer spriteRenderer = figura.GetComponent<SpriteRenderer>();
            if (spriteRenderer != null)
            {
                if (spriteRenderer.sortingOrder != sortingOrderCorrectos[i])
                {
                    return false; // Si el sortingOrder no es el correcto, el juego no está completo
                }
            }
        }

        // Si todas las figuras están en el panelDestino, con la posición y el sortingOrder correctos
        return true;
    }

    // Mostrar el mensaje de "Completado" y pausar el juego
    private void MostrarCompletado()
    {
        if (textoCompletado != null)
        {
            textoCompletado.gameObject.SetActive(true); // Activar el texto de "Completado"
            Time.timeScale = 0f; // Pausar el juego (al poner el timeScale a 0)
        }
    }
    public void SubirCapaFigura()
    {
        if (ultimaFiguraSeleccionada != null)
        {
            // Subir la figura en la capa (aumentar el sortingOrder) pero restringido a los límites
            SpriteRenderer spriteRenderer = ultimaFiguraSeleccionada.GetComponent<SpriteRenderer>();
            if (spriteRenderer != null)
            {
                // Aumentar y clamped (limitado entre minSortingOrder y maxSortingOrder)
                spriteRenderer.sortingOrder = Mathf.Clamp(spriteRenderer.sortingOrder + 1, minSortingOrder, maxSortingOrder);
            }
        }
    }

    public void BajarCapaFigura()
    {
        if (ultimaFiguraSeleccionada != null)
        {
            // Bajar la figura en la capa (disminuir el sortingOrder) pero restringido a los límites
            SpriteRenderer spriteRenderer = ultimaFiguraSeleccionada.GetComponent<SpriteRenderer>();
            if (spriteRenderer != null)
            {
                // Disminuir y clamped (limitado entre minSortingOrder y maxSortingOrder)
                spriteRenderer.sortingOrder = Mathf.Clamp(spriteRenderer.sortingOrder - 1, minSortingOrder, maxSortingOrder);
            }
        }
    }    
}
