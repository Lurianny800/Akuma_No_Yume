using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player : MonoBehaviour
{
    public float moveSpeed;
    private bool moving;
    public Vector2 input;

    private void Update()
    {
       if (moving != true)
        {
            input.x = Input.GetAxisRaw("Horizontal");
            input.y = Input.GetAxisRaw("Vertical");

            if (input.x != 0)
            {
                input.y = 0;
            }

            if (input != Vector2.zero)
            {
                var targetPosition = transform.position;

                targetPosition.x += input.x;
                targetPosition.y += input.y;

                StartCoroutine(Move(targetPosition));
            }

        }
    }

    IEnumerator Move(Vector3 targetPosition)
    {
        moving = true;
        while ((targetPosition - transform.position).sqrMagnitude > Mathf.Epsilon)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);
            yield return null;
        }

        transform.position = targetPosition;
        moving = false;
    }

}
