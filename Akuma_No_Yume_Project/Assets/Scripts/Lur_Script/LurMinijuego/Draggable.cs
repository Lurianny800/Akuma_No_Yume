using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;

public class Draggable : MonoBehaviour, IPointerDownHandler, IDragHandler, IEndDragHandler
{
    private RectTransform rectTransform;
    private Vector2 offset;
    private bool isDragging = false;
    public float gridSize = 100f; // Tamaño de la celda en el grid
    public RectTransform panelDestino; // Referencia al PanelDestino en la UI
    private bool isInPanelDestino = false; // Para saber si está en el PanelDestino


    // Límites personalizados (ajústalos en el Inspector de Unity)
    public float minX = -5f, maxX = 5f, minY = -3f, maxY = 3f;

    void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
    }
  
    public void OnPointerDown(PointerEventData eventData)
    {
        if (!isInPanelDestino) return; // No hacer nada si no está en el PanelDestino

        // 📌 Calcular el offset correctamente antes de arrastrar
        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            rectTransform.parent as RectTransform,
            eventData.position,
            eventData.pressEventCamera,
            out offset
        );
    }
    public void OnBeginDrag(PointerEventData eventData)
    {
        if (!isDragging) return; // No arrastrar si no está en el PanelDestino
    }
    public void OnDrag(PointerEventData eventData)
    {
        if (!isDragging) return;

        Vector2 localPointerPosition;
        if (RectTransformUtility.ScreenPointToLocalPointInRectangle(
            panelDestino, // 📌 Asegurar que la posición es relativa a `panelDestino`
            eventData.position,
            eventData.pressEventCamera,
            out localPointerPosition))
        {
            // 📌 Ajustar al grid
            Vector2 newPosition = SnapToGrid(localPointerPosition - offset);

            // 📌 Restringir dentro de `panelDestino`
            newPosition = ClampToBounds(newPosition);

            rectTransform.anchoredPosition = newPosition;
        }
    }
    // 📌 Método para ajustar la posición al grid
    private Vector2 SnapToGrid(Vector2 position)
    {
        float snappedX = Mathf.Round(position.x / gridSize) * gridSize;
        float snappedY = Mathf.Round(position.y / gridSize) * gridSize;
        return new Vector2(snappedX, snappedY);
    }
    // 📌 Método para restringir la figura dentro de `panelDestino`
    private Vector2 ClampToBounds(Vector2 position)
    {
        Vector2 minBounds = new Vector2(panelDestino.rect.xMin, panelDestino.rect.yMin);
        Vector2 maxBounds = new Vector2(panelDestino.rect.xMax, panelDestino.rect.yMax);

        // Restringir dentro de los límites personalizados
        float clampedX = Mathf.Clamp(position.x, minX, maxX);
        float clampedY = Mathf.Clamp(position.y, minY, maxY);


        return new Vector2(clampedX, clampedY);
    }
    public void OnEndDrag(PointerEventData eventData)
    {
        // Nada especial al soltar, solo asegurarse de que sigue en los límites del Canvas si es necesario
    }
    // 📌 Método para mover al `PanelDestino` y activar el arrastre
    public void MoveToPanel(RectTransform panelDestino)
    {
        isDragging = true; // 🔹 Ahora sí se puede arrastrar
        rectTransform.SetParent(panelDestino, false);

        // 📌 Centrar la figura en `panelDestino`
        rectTransform.anchorMin = new Vector2(0.5f, 0.5f);
        rectTransform.anchorMax = new Vector2(0.5f, 0.5f);
        rectTransform.pivot = new Vector2(0.5f, 0.5f);
        // Restaurar la posición dentro del panel de selección
        rectTransform.anchoredPosition = Vector2.zero;
    } 
}