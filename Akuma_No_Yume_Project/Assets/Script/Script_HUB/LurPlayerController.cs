using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LurPlayerController : MonoBehaviour
{    
    public float moveSpeed = 5f;
    public float forcejump;
    public float saltosMaximos;
    public LayerMask capaSuelo;

    private Rigidbody2D rb;
    private BoxCollider2D boxCollider;
    private float saltosRestantes;
    // Start is called before the first frame update
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        boxCollider = GetComponent<BoxCollider2D>();
        saltosRestantes = saltosMaximos;
    }

    // Update is called once per frame
    void Update()
    {
        Movimiento();
        Salto();
    }
    bool EstaEnSuelo()
    {
        RaycastHit2D raycastHit = Physics2D.BoxCast(boxCollider.bounds.center, new Vector2(boxCollider.bounds.size.x, boxCollider.bounds.size.y), 0f, Vector2.down, 0.2f, capaSuelo);
        return raycastHit.collider != null;
    }

    void Movimiento() //Movimiento físico controlado sin pérdida de precisión.
    {
        float moveX = Input.GetAxis("Horizontal");
        
        rb.velocity = new Vector2 (moveX * moveSpeed, rb.velocity.y);
    }
    void Salto() 
    {
        if (EstaEnSuelo())
        {
            saltosRestantes = saltosMaximos;
        }

        if (Input.GetKeyDown(KeyCode.Space) && saltosRestantes > 0)
        {
            saltosRestantes--;
            rb.velocity = new Vector2(rb.velocity.x, 0f);
            rb.AddForce(Vector2.up * forcejump, ForceMode2D.Impulse);
        }
    }
}
