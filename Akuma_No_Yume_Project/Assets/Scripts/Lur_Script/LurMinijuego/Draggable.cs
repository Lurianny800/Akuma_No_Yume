using UnityEngine;

public class Draggable : MonoBehaviour
{
    private Vector3 offset;
    private bool isDragging = false;
    public float gridSize = 1.0f; // Tamaño de la celda en el grid

    private void Start()
    {
        // Aseguramos que la figura comience bien posicionada
        ResetPosition();
    }

    private void OnMouseDown()
    {
        if (GetComponent<Collider2D>() == null)
        {
            Debug.LogError("El objeto no tiene un Collider2D. No podrá ser arrastrado.");
            return;
        }

        // Calculamos el offset basado en la posición actual del objeto y el mouse
        offset = transform.position - GetMouseWorldPosition();
        isDragging = true;

        Debug.Log("Iniciando el arrastre");
    }

    private void OnMouseDrag()
    {
        if (isDragging)
        {
            Vector3 mousePos = GetMouseWorldPosition() + offset;

            // Redondeamos la posición para que se mueva en pasos de grid
            float x = Mathf.Round(mousePos.x / gridSize) * gridSize;
            float y = Mathf.Round(mousePos.y / gridSize) * gridSize;

            // Asignamos la nueva posición ajustada al grid
            transform.position = new Vector3(x, y, 0f);

            Debug.Log($"Posición ajustada al grid: {transform.position}");
        }
    }

    private void OnMouseUp()
    {
        isDragging = false;
    }

    // Método para obtener la posición del mouse en coordenadas de mundo
    private Vector3 GetMouseWorldPosition()
    {
        Vector3 mousePosition = Input.mousePosition;
        mousePosition.z = -Camera.main.transform.position.z; // Para que se mantenga en 2D
        return Camera.main.ScreenToWorldPoint(mousePosition);
    }

    // Método para resetear la posición de la figura al centro de la cámara
    public void ResetPosition()
    {
        Vector3 worldCenter = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width / 2, Screen.height / 2, -Camera.main.transform.position.z));
        worldCenter.z = 0; // Nos aseguramos de que la figura se mantenga en 2D

        transform.position = worldCenter;

        Debug.Log($"Figura reposicionada en: {transform.position}");
    }
}
