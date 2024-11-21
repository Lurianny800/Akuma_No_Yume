using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class path : MonoBehaviour
{
    public Transform posicionInicial;
    SpriteRenderer spriteRenderer;

    private void Awake()
    {
        
    }

    void OnTriggerEnter2D(Collider2D other)
    {      

        if (other.gameObject.tag == "Player")
        {
            Debug.Log("Está dentro el player");

            StartCoroutine(Respawn(0.75f));

            IEnumerator Respawn(float duration)
            {
                
                yield return new WaitForSeconds(duration);
                other.gameObject.transform.position = posicionInicial.position;
            }                          

        }
    }    

    
}