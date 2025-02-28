using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPositionLoader : MonoBehaviour
{
    void Start()
    {
        if (gameObject.CompareTag("Player")) // Solo afecta al objeto con el tag "Player"
        {
            string previousScene = PlayerPrefs.GetString("PreviousScene", ""); // Obtener la escena anterior

            if (previousScene == "game1") // Solo mover si vienes de Scena B
            {
                if (PlayerPrefs.HasKey("PlayerX") && PlayerPrefs.HasKey("PlayerY"))
                {
                    float x = PlayerPrefs.GetFloat("PlayerX");
                    float y = PlayerPrefs.GetFloat("PlayerY");
                    transform.position = new Vector2(x, y);
                }
            }
        }
    }
}
