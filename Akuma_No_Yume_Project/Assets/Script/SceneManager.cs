using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;//Librería de escenas

public class SceneManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }
    public void ChangeScenes(string NombreEscena) //Invocar al inspector
    {
        SceneManager.LoadScene(NombreEscena); //Cargará la escena que escribamos
    }

    // Update is called once per frame
    void Update()
    {

    }
}
