using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Lur_GameManager : MonoBehaviour
{
    public static Lur_GameManager Instance;

    public event Action OnStateChanged; // Evento para notificar cambios
    public bool activateScriptA = false;
    public bool deactivateScriptB = false;  

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // Mantener este objeto entre escenas
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void SetScriptsState(bool activateA, bool deactivateB)
    {
        activateScriptA = activateA;
        deactivateScriptB = deactivateB;

        OnStateChanged?.Invoke(); // Notificar a los objetos en la otra escena
    }
}
