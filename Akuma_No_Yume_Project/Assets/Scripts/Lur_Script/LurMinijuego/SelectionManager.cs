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
    public Button botonSubirCapa; // Botón para subir la figura de capa
    public Button botonBajarCapa; // Botón para bajar la figura de capa
    private float defaultZPosition = 0; // Almacenar el valor predeterminado de la capa

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
            // Al seleccionar la figura, almacenamos su posición Z original
            defaultZPosition = figuraSeleccionada.transform.localPosition.z;
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

            // Restaurar la posición Z original
            ultimaFiguraSeleccionada.transform.localPosition = new Vector3(ultimaFiguraSeleccionada.transform.localPosition.x, ultimaFiguraSeleccionada.transform.localPosition.y, defaultZPosition);

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
    public void SubirCapaFigura()
    {
        if (ultimaFiguraSeleccionada != null)
        {
            // Subir la figura en el eje Z (hacerla más cercana al "frente")
            Vector3 newPosition = ultimaFiguraSeleccionada.transform.localPosition;
            newPosition.z += 1f; // Aumentar la posición Z
            ultimaFiguraSeleccionada.transform.localPosition = newPosition;
        }
    }

    public void BajarCapaFigura()
    {
        if (ultimaFiguraSeleccionada != null)
        {
            // Bajar la figura en el eje Z (hacerla más alejada)
            Vector3 newPosition = ultimaFiguraSeleccionada.transform.localPosition;
            newPosition.z -= 1f; // Disminuir la posición Z
            ultimaFiguraSeleccionada.transform.localPosition = newPosition;
        }
    }
}
