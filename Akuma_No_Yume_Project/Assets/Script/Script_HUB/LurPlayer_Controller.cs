using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LurPlayer_Controller : MonoBehaviour
{
    public float fps = 5f;
    public float moveSpeed = 5f;
    public Rigidbody rb;
    public float moveForce = 10f;
    public CharacterController controller;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void Movimiento3() //Movimiento físico controlado sin pérdida de precisión.
    {
        float moveX = Input.GetAxis("Horizontal");
        float moveY = Input.GetAxis("Vertical"); ;
        Vector3 move = new Vector3(moveX, moveY, 0) * moveSpeed * Time.deltaTime; rb.MovePosition(rb.position + move);
    }
}
