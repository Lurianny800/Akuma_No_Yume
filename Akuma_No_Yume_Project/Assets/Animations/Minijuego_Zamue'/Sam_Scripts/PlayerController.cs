using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f;
    public LayerMask pushableLayer;
    public LayerMask obstacleLayer;
    private bool isMoving = false;

    void Update()
    {
        if (isMoving) return; // Evita que el jugador mueva dos cosas a la vez

        Vector2 moveDirection = Vector2.zero;

        if (Input.GetKeyDown(KeyCode.W)) moveDirection = Vector2.up;
        if (Input.GetKeyDown(KeyCode.S)) moveDirection = Vector2.down;
        if (Input.GetKeyDown(KeyCode.A)) moveDirection = Vector2.left;
        if (Input.GetKeyDown(KeyCode.D)) moveDirection = Vector2.right;

        if (moveDirection != Vector2.zero)
        {
            AttemptMove(moveDirection);
        }
    }

    void AttemptMove(Vector2 direction)
    {
        Vector2 newPosition = (Vector2)transform.position + direction;

        Collider2D pushableObject = Physics2D.OverlapCircle(newPosition, 0.1f, pushableLayer);
        if (pushableObject != null)
        {
            PushableObject pushable = pushableObject.GetComponent<PushableObject>();

            // Verificar si el objeto empujable puede moverse
            if (pushable != null && pushable.Move(direction))
            {
                StartCoroutine(SmoothMove(newPosition)); // Mueve al jugador solo si el objeto fue empujado
            }
        }
        else
        {
            StartCoroutine(SmoothMove(newPosition));
        }
    }

    private IEnumerator SmoothMove(Vector2 target)
    {
        isMoving = true;
        Vector2 start = transform.position;
        float elapsedTime = 0f;
        float duration = 1f / moveSpeed;

        while (elapsedTime < duration)
        {
            transform.position = Vector2.Lerp(start, target, elapsedTime / duration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        transform.position = target;
        isMoving = false;
    }
}
