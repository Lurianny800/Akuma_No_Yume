using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lur_ScriptsManager : MonoBehaviour
{
    public MonoBehaviour scriptA; // Asigna el script que quieres activar
    public MonoBehaviour scriptB; // Asigna el script que quieres desactivar

    private void Start()
    {
        // Aplicar estado inicial por si el botón ya se presionó antes
        ApplyState();

        // Suscribirse al evento de cambios
        Lur_GameManager.Instance.OnStateChanged += ApplyState;
    }

    private void OnDestroy()
    {
        // Desuscribirse del evento al destruirse para evitar errores
        Lur_GameManager.Instance.OnStateChanged -= ApplyState;
    }

    private void ApplyState()
    {
        if (Lur_GameManager.Instance.activateScriptA)
        {
            scriptA.enabled = true;
        }

        if (Lur_GameManager.Instance.deactivateScriptB)
        {
            scriptB.enabled = false;
        }
    }
}
