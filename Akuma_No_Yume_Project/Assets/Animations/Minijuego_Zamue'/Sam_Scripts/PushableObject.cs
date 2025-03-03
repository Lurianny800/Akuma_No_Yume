using System.Collections;
using UnityEngine;

public class PushableObject : MonoBehaviour
{
    public float moveSpeed = 5f; // Velocidad de transición
    private bool isMoving = false;
    public LayerMask obstacleLayer; // Capa de paredes
    public LayerMask pushableLayer; // Capa de objetos empujables

    public bool Move(Vector2 direction)
    {
        if (isMoving) return false; // No moverse si ya está en movimiento

        Vector2 startPosition = transform.position;
        Vector2 targetPosition = FindFarthestValidPosition(startPosition, direction);

        if (targetPosition != startPosition) // Solo moverse si hay una diferencia
        {
            StartCoroutine(SmoothMove(targetPosition));
            return true;
        }

        return false;
    }

    private Vector2 FindFarthestValidPosition(Vector2 startPosition, Vector2 direction)
    {
        Vector2 currentPosition = startPosition;
        Vector2 nextPosition = currentPosition + direction;

        // Avanza hasta que encuentre un obstáculo o un objeto empujable
        while (!IsBlocked(nextPosition))
        {
            currentPosition = nextPosition;
            nextPosition += direction;
        }

        return currentPosition; // Última posición válida antes de chocar
    }

    private bool IsBlocked(Vector2 position)
    {
        return Physics2D.OverlapCircle(position, 0.1f, obstacleLayer) ||
               Physics2D.OverlapCircle(position, 0.1f, pushableLayer);
    }

    private IEnumerator SmoothMove(Vector2 target)
    {
        isMoving = true;
        Vector2 start = transform.position;
        float elapsedTime = 0f;
        float duration = Vector2.Distance(start, target) / moveSpeed; // Ajustar duración según la distancia

        while (elapsedTime < duration)
        {
            transform.position = Vector2.Lerp(start, target, elapsedTime / duration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        transform.position = target; // Asegurar que termine en la celda correcta
        isMoving = false;
    }
}
