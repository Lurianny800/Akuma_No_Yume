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
    [HideInInspector]public GameObject ultimaFiguraEnDestino; // Última figura seleccionada dentro del panelDestino


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
    [Header("Límites de Orden en la Capa")]
    [Tooltip("Valor mínimo permitido para el sortingOrder")]
    public int minLayer = 0;
    [Tooltip("Valor máximo permitido para el sortingOrder")]
    public int maxLayer = 3;

    //Variables privadas.
    private int defaultSortingOrder; // Almacenar el valor de sortingOrder original
    private bool puedeCambiarCapa = true;
    private float tiempoEspera = 0.1f; // 100ms de espera

    void Start()
    {
        // Configurar botones si están asignados
        if (botonRemover != null) botonRemover.onClick.AddListener(RemoverUltimaFigura);
        if (botonSubirCapa != null) botonSubirCapa.onClick.AddListener(SubirCapaFigura);
        if (botonBajarCapa != null) botonBajarCapa.onClick.AddListener(BajarCapaFigura);
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

        if (index < 0 || index >= figuras.Length)
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
        }
        ultimaFiguraSeleccionada = ultimaFiguraEnDestino = figuraSeleccionada;
        // 📌 Almacenar su sortingOrder original
        SpriteRenderer spriteRenderer = figuraSeleccionada.GetComponent<SpriteRenderer>();
        if (spriteRenderer != null)
        {
            defaultSortingOrder = spriteRenderer.sortingOrder;
        }
    }

    public void RemoverUltimaFigura()
    {
        if (ultimaFiguraEnDestino != null)
        {
            // Remover la última figura en el panelDestino
            ultimaFiguraEnDestino.transform.SetParent(panelSeleccion, false);

            // Restaurar su posición dentro del panel de selección
            ultimaFiguraEnDestino.GetComponent<RectTransform>().anchoredPosition = Vector2.zero;

            // Restaurar el sortingOrder original usando SpriteRenderer
            SpriteRenderer spriteRenderer = ultimaFiguraEnDestino.GetComponent<SpriteRenderer>();
            if (spriteRenderer != null)
            {
                spriteRenderer.sortingOrder = minLayer;
            }

            // Desactivar Draggable en lugar de eliminarlo
            Draggable draggable = ultimaFiguraEnDestino.GetComponent<Draggable>();
            if (draggable != null)
            {
                draggable.MoveToPanel(panelSeleccion);
            }

            // Limpiar la referencia de la última figura en destino
            ultimaFiguraEnDestino = null;
        }
        else
        {
            Debug.LogWarning("No hay figura en el panel destino para remover.");
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
    // Subir la capa de la última figura seleccionada dentro del panelDestino
    public void SubirCapaFigura()
    {
        if (puedeCambiarCapa && ultimaFiguraEnDestino != null)
        {
            SpriteRenderer spriteRenderer = ultimaFiguraEnDestino.GetComponent<SpriteRenderer>();
            if (spriteRenderer != null)
            {
                int nuevaCapa = spriteRenderer.sortingOrder + 1;

                if (nuevaCapa <= maxLayer)
                {
                    spriteRenderer.sortingOrder = nuevaCapa;
                    Debug.Log($"Subiendo capa: {spriteRenderer.sortingOrder}");
                }
            }
            StartCoroutine(EsperarCambioDeCapa());
        }
    }

    public void BajarCapaFigura()
    {
        if (puedeCambiarCapa && ultimaFiguraEnDestino != null)
        {
            SpriteRenderer spriteRenderer = ultimaFiguraEnDestino.GetComponent<SpriteRenderer>();
            if (spriteRenderer != null)
            {
                int nuevaCapa = spriteRenderer.sortingOrder - 1;

                if (nuevaCapa >= minLayer)
                {
                    spriteRenderer.sortingOrder = nuevaCapa;
                    Debug.Log($"Bajando capa: {spriteRenderer.sortingOrder}");
                }
            }
            StartCoroutine(EsperarCambioDeCapa());
        }
    }

    private IEnumerator EsperarCambioDeCapa()
    {
        puedeCambiarCapa = false;
        yield return new WaitForSeconds(tiempoEspera);
        puedeCambiarCapa = true;
    }
}
