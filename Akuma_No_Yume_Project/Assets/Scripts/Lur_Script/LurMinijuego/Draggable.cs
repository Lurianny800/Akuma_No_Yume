using UnityEngine;

public class Draggable : MonoBehaviour
{
    private Vector3 offset;
    private bool isDragging = false;
    public float gridSize = 1.0f; // Tamaño de la celda en el grid

    // Límites personalizados (ajústalos en el Inspector de Unity)
    public float minX = -5f, maxX = 5f, minY = -3f, maxY = 3f;

    private void OnMouseDown()
    {
        if (GetComponent<Collider2D>() == null)
        {
            Debug.LogError("El objeto no tiene un Collider2D.");
            return;
        }

        offset = transform.position - Camera.main.ScreenToWorldPoint(Input.mousePosition);
        isDragging = true;
    }

    private void OnMouseDrag()
    {
        if (isDragging)
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
        AdjustToGrid();
    }

    private void AdjustToGrid()
    {
        float x = Mathf.Round(transform.position.x / gridSize) * gridSize;
        float y = Mathf.Round(transform.position.y / gridSize) * gridSize;
        transform.position = new Vector3(x, y, 0f);
    }

    public void SetMovementLimits(float newMinX, float newMaxX, float newMinY, float newMaxY)
    {
        minX = newMinX;
        maxX = newMaxX;
        minY = newMinY;
        maxY = newMaxY;
    }
    public void CenterFigure(Canvas canvas)
    {
        // Obtener la posición central de la cámara en el mundo (centrado en la cámara)
        Vector3 screenCenter = new Vector3(Screen.width / 2, Screen.height / 2, 0);
        Vector3 worldCenter = Camera.main.ScreenToWorldPoint(new Vector3(screenCenter.x, screenCenter.y, Camera.main.nearClipPlane));
        worldCenter.z = 0;

        // Si el canvas está en 'Screen Space' o 'World Space', manejamos la posición
        if (canvas.renderMode == RenderMode.ScreenSpaceOverlay || canvas.renderMode == RenderMode.ScreenSpaceCamera)
        {
            // Convertimos la posición del centro de la cámara (en pantalla) a las coordenadas del Canvas
            RectTransformUtility.ScreenPointToWorldPointInRectangle(canvas.transform as RectTransform, screenCenter, Camera.main, out worldCenter);
        }

        // Establecemos la nueva posición de la figura en el centro de la cámara (dentro del Canvas)
        transform.position = worldCenter;
    }
}
