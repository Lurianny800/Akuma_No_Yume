using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lur_GiftSkin : MonoBehaviour
{
    [Tooltip("Tiempo que tarda la animación en completarse")]
    public float animationDuration = 1.0f;

    [Tooltip("Tamaño final del ítem")]
    public Vector3 finalScale = new Vector3(2f, 2f, 2f);

    [Tooltip("Referencia al Animator del ítem")]
    public Animator animator;

    private bool isCollected = false;
    private SpriteRenderer spriteRenderer;
    private Transform player;
    private Vector3 initialScale;
    private float timer = 0f;
    private Camera mainCamera;
    private void Start()
    {
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        initialScale = transform.localScale;
        mainCamera = Camera.main; 
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !isCollected)
        {
            isCollected = true;
            player = other.transform;
            GetComponent<Collider2D>().enabled = false; // Desactivar colisión

            if (animator != null)
            {
                animator.SetBool("isPickedUp", true); // Activar animación del sprite
            }

            StartCoroutine(AnimatePickup());
        }
    }

    private System.Collections.IEnumerator AnimatePickup()
    {
        Vector3 startPosition = transform.position;
        while (timer < animationDuration)
        {
            timer += Time.deltaTime;
            float progress = timer / animationDuration;

            // Obtener el centro de la pantalla en coordenadas de mundo
            Vector3 screenCenter = mainCamera.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, mainCamera.nearClipPlane + 5f));

            // Mover hacia el centro de la cámara dinámicamente
            transform.position = Vector3.Lerp(startPosition, screenCenter, progress);

            // Escalar el objeto
            transform.localScale = Vector3.Lerp(initialScale, finalScale, progress);

            // Hacer que se desvanezca
            if (spriteRenderer != null)
            {
                Color color = spriteRenderer.color;
                color.a = Mathf.Lerp(1f, 0f, progress);
                spriteRenderer.color = color;
            }

            yield return null;
        }

        Destroy(gameObject); // Eliminar el objeto tras la animación
    }

}
