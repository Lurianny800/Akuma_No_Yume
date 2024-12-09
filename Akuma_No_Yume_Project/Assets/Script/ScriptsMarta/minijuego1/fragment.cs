using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fragment : MonoBehaviour
{
    public GameObject congratsMessage;

    private void OnTriggerEnter2D()
    {        
        congratsMessage.gameObject.SetActive(true);
    }
}
