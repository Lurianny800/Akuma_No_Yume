using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
    public float moveSpeed = 5f; // Velocidad de movimiento

    private void Update()
    {
        // Obtener entradas de movimiento con las flechas del teclado
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        // Crear un vector de dirección
        Vector2 direction = new Vector2(horizontal, vertical).normalized;

        // Mover al personaje
        transform.Translate(direction * moveSpeed * Time.deltaTime);
    }
}
