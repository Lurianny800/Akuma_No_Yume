using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class buttons : MonoBehaviour
{
    public void ResetLevel()
    {
        SceneManager.LoadScene("game1");
        Time.timeScale = 1;
    }

    public void FinJuego()
    {
        Application.Quit(); //cierra la aplicación
        Debug.Log("Juego finalizado"); //mensaje que me salta en unity para probar si funciona
    }
}
