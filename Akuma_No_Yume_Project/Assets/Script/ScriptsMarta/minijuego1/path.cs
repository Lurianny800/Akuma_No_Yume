using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CompositeCollider2D))]
public class path : MonoBehaviour
{
    public Transform posicionInicial;

    void OnTriggerEnter2D(Collider2D other)
    {      

        if (other.gameObject.tag == "Player")
        {
            Debug.Log("You fell :(");

            StartCoroutine(Respawn(0.5f));
           
            IEnumerator Respawn(float duration)
            {
                

                yield return new WaitForSeconds(duration);
                
                other.gameObject.transform.position = posicionInicial.position;
             
            }           
        }
    }        
}