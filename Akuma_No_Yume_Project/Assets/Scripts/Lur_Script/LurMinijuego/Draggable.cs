using System.Collections;
using UnityEngine;

public class Draggable : MonoBehaviour
{
    private Vector3 offset;
    private bool isDragging = false;
    public float gridSize = 1.0f; // Tamaño de la celda en el grid
    public RectTransform panelSeleccion; // Panel de selección para verificar
    public RectTransform panelDestino; // Panel donde se puede arrastrar la figura


    // Límites personalizados (ajústalos en el Inspector de Unity)
    public float minX = -5f, maxX = 5f, minY = -3f, maxY = 3f;
    void Update()
    {
        // Solo permitir el arrastre si la figura está en el panelDestino
        if (panelDestino != null && panelDestino.rect.Contains(panelDestino.InverseTransformPoint(transform.position)))
        {
            if (Input.GetMouseButtonDown(0)) // Solo si se hace clic con el ratón
            {
                offset = transform.position - Camera.main.ScreenToWorldPoint(Input.mousePosition);
                isDragging = true;
            }

            if (isDragging)
            {
                Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition) + offset;
                transform.position = mousePos;
            }

            if (Input.GetMouseButtonUp(0)) // Cuando se suelta el clic
            {
                isDragging = false;
            }
        }
    }
    private void OnMouseDown()
    {
        if (GetComponent<Collider2D>() == null)
        {
            Debug.LogError("El objeto no tiene un Collider2D.");
            return;
        }

        // Solo activar el arrastre si el objeto está dentro del panelDestino
        if (panelDestino.rect.Contains(panelDestino.InverseTransformPoint(transform.position)))
        {
            offset = transform.position - Camera.main.ScreenToWorldPoint(Input.mousePosition);
            isDragging = true;
        }
    }

    private void OnMouseDrag()
    {
        // Asegurarse de que solo se mueva si está en el panelDestino, no en el panelSeleccion
        if (isDragging && panelDestino.rect.Contains(panelDestino.InverseTransformPoint(transform.position)))
        {
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition) + offset;
            mousePos.z = 0;

            // Aplicar los límites definidos por el usuario
            mousePos.x = Mathf.Clamp(mousePos.x, minX, maxX);
            mousePos.y = Mathf.Clamp(mousePos.y, minY, maxY);

            transform.position = mousePos;
        }
    }

    private void OnMouseUp()
    {
        isDragging = false;
        transform.position = new Vector3(
            Mathf.Clamp(Mathf.Round(transform.position.x / gridSize) * gridSize, minX, maxX),
            Mathf.Clamp(Mathf.Round(transform.position.y / gridSize) * gridSize, minY, maxY), 0f);
    }    
}