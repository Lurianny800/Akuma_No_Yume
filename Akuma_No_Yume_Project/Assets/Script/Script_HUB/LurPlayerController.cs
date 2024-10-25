using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LurPlayerController : MonoBehaviour
{    
    public float moveSpeed = 5f;
    public float forcejump;
    public Rigidbody2D rb;
    // Start is called before the first frame update
    private void Start()
    {
       rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        Movimiento();
        Salto();
    }
    void Movimiento() //Movimiento físico controlado sin pérdida de precisión.
    {
        float moveX = Input.GetAxis("Horizontal");
        
        rb.velocity = new Vector2 (moveX * moveSpeed, rb.velocity.y);
    }
    void Salto() 
    {
        if (Input.GetKeyDown(KeyCode.Space)) 
        {
            rb.AddForce(Vector2.up * forcejump, ForceMode2D.Impulse);
        }
    }
}
