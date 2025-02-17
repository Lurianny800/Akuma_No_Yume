using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class SelectionManager : MonoBehaviour
{
    public GameObject[] figuras; // Figuras disponibles en el panel de selección
    public RectTransform panelSeleccion; // Panel donde están las figuras
    private GameObject ultimaFiguraSeleccionada; // Referencia a la última figura seleccionada por el usuario
    public Button botonRemover; // Botón de remover
    public Canvas canvasDestino;

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

        // Seleccionar la figura que se encuentra en el índice
        GameObject figuraSeleccionada = figuras[index];

        // Asegurarse de que la figura no esté seleccionada previamente
        if (figuraSeleccionada == ultimaFiguraSeleccionada) return;

        // Si la figura ya está en uso, no hacer nada
        if (figuraSeleccionada != null)
        {
            // Llamar al método para centrar la figura en el canvas
            CenterFigureInCanvas(canvasDestino, figuraSeleccionada);
        }

        // Registrar la última figura seleccionada
        ultimaFiguraSeleccionada = figuraSeleccionada;
    }   
    // Centra la figura en el Canvas
    private void CenterFigureInCanvas(Canvas canvas, GameObject figure)
    {
        if (canvas == null || figure == null) return;

        RectTransform canvasRect = canvas.GetComponent<RectTransform>();
        Vector3 screenCenter = new Vector3(Screen.width / 2, Screen.height / 2, 0);

        // Convertir la posición de la pantalla (centro) a las coordenadas del mundo
        Vector3 worldCenter = Camera.main.ScreenToWorldPoint(new Vector3(screenCenter.x, screenCenter.y, Camera.main.nearClipPlane));
        worldCenter.z = 0;  // Asegurarse de que la z no se vea afectada

        // Si el canvas está en ScreenSpace, ajustamos la posición
        if (canvas.renderMode == RenderMode.ScreenSpaceOverlay || canvas.renderMode == RenderMode.ScreenSpaceCamera)
        {
            // Convertir de las coordenadas de la pantalla a las del mundo, respetando el Canvas
            RectTransformUtility.ScreenPointToWorldPointInRectangle(canvasRect, screenCenter, Camera.main, out worldCenter);
        }

        // Establecer la nueva posición en el RectTransform de la figura seleccionada
        figure.transform.position = worldCenter;
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
}
