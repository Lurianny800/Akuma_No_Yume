using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lur_Patatas : MonoBehaviour
{
    public Lur_DialogueIntro dialogueSystem; // Referencia al sistema de diálogo

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            dialogueSystem.StartDialogue2(); // Activa el segundo diálogo
            gameObject.SetActive(false); // Desactiva el objeto después de ser tocado
        }
    }
}
