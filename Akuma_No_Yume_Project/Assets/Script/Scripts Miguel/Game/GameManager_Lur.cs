using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager_Lur : MonoBehaviour
{
    public static GameManager_Lur instance;
    [HideInInspector]public int score = 0;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject); // Asegurarte de que persista entre escenas
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void AddScore(int points)
    {
        score += points;
        Debug.Log("Coleccionable: " + score);
    }
}
