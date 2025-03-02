using UnityEngine;

public class ChangePlayerPosition : MonoBehaviour
{
    public Vector2 newPosition; // Nueva posición a la que se moverá el jugador

    public void ChangerPlayerPosition()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            player.transform.position = newPosition;
        }
        else
        {
            Debug.LogWarning("No se encontró el objeto con tag 'Player'.");
        }
    }
}
