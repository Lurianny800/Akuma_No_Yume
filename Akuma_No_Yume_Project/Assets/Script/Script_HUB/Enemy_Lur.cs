using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Lur : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            HUB_Manager.Instance.PerderVida();
        }
    }
}
