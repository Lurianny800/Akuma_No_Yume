using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class player : MonoBehaviour
{
    [Header("Movement")]
    [Tooltip("Adjust player's movement speed")]
    public float moveSpeed;    
    private bool moving;
    [HideInInspector]public Vector2 input;

    [Space]
    public healthHUD_Marta vidasHUB; //Llamar al panel

    [Header("Health")]
    [Tooltip("Adjust player's max health")]
    [SerializeField] private int maxHealth = 5;    
    [Space]
    [Tooltip("Shows player's current health")]
    [SerializeField] private int currentHealth;
    

    private void Start()
    {
        currentHealth = maxHealth;
    }

    public void RemoveHealth()
    {
        currentHealth--;
        vidasHUB.DesactivarVida(currentHealth);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Enemy" || other.gameObject.tag == "Path")
        {
            RemoveHealth();
        }        
        
    }

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
