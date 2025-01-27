using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemigo : MonoBehaviour
{
    [Header("Movement")]
    [Tooltip("Set your enemy's start posititon")]
    public Vector3 startPosition;
    [Space]
    [Tooltip("Add the positions your enemy will move to")]
    public Vector3[] moveToPoints;
    
    [HideInInspector]public Vector3 currentPoint;
    [Space]
    [Tooltip("Adjust your enemy's movement speed")]
    public float moveSpeed;

    [HideInInspector]public int pointSelection;

    // Start is called before the first frame update
    void Start()
    {
        //Sets the object to your starting point
        this.transform.position = startPosition;

    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }

    void Move()
    {
        //Starts to move the object towards the first "moveToPoint" you set in inspector
        this.transform.position = Vector3.MoveTowards(this.transform.position, currentPoint, Time.deltaTime * moveSpeed);

        //check to see if the object is at the next "moveToPoint"
        if (this.transform.position == currentPoint)
        {

            //if so it sets the next moveTo location
            pointSelection++;

            //if your object hits the last "moveToPoint it sends the object back to starting position to start the sequence over
            if (pointSelection == moveToPoints.Length)
            {
                pointSelection = 0;

            }

            //sets the destination of the "moveToPoint" destination
            currentPoint = moveToPoints[pointSelection];
        }
    }
    [Header("Player respawn")]
    [Tooltip("Set your player's respawn point")]
    public Transform posicionInicial;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            Debug.Log("You fell :(");

            // Obtener el script del jugador
            player playerScript = other.gameObject.GetComponent<player>();
            if (playerScript != null)
            {
                playerScript.EnableInput(false); // Deshabilitar el input
            }

            // Congelar el tiempo
            Time.timeScale = 0;

            // Iniciar la corrutina de respawn y pasar el script del jugador
            StartCoroutine(Respawn(0.5f, other.gameObject, playerScript));
        }
    }

    IEnumerator Respawn(float duration, GameObject player, player playerScript)
    {
        // Esperar en tiempo real (independientemente del Time.timeScale)
        float elapsed = 0;
        while (elapsed < duration)
        {
            elapsed += Time.unscaledDeltaTime; // Esperar sin ser afectado por Time.timeScale
            yield return null;
        }

        // Reposicionar al jugador
        player.transform.position = posicionInicial.position;

        // Verificar si el jugador aún tiene vidas
        if (playerScript.currentHealth > 0)
        {
            // Solo reanudar el tiempo si el jugador tiene vidas
            Time.timeScale = 1;

            // Reactivar el input
            playerScript.EnableInput(true);
        }
        else
        {
            // Si el jugador está muerto (0 vidas), no reanudar el tiempo
            Debug.Log("Game Over: El jugador está muerto.");
        }
    }

}
