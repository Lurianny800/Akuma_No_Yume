using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(BoxCollider2D))]
public class Lur_PlayerMovement2D : MonoBehaviour
{
    [Header("Movimiento")]
    public float moveSpeed = 5f;
    public float jumpForce = 16f;
    public int maxJumps = 1;

    private Rigidbody2D rb;
    private Animator anim;
    private int jumpsRemaining;
    private bool isGrounded;
    private bool isGrounded2;
    private bool isGrounded3;
    private Vector3 originalScale;
    private bool puedeCurarse = false;

    [Header("Chequeo de suelo")]
    public Transform groundCheck;
    public Transform groundCheck2;
    public Transform groundCheck3;
    public float groundCheckDistance = 0.2f;
    public LayerMask groundLayer;

    [Header("Dash")]
    public float dashSpeed = 12f;
    public float dashDuration = 0.2f;
    public float dashCooldown = 1.5f;
    private bool canDash = false;
    private bool isDashing = false;
    private float lastDashTime = -Mathf.Infinity;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        jumpsRemaining = maxJumps;
        originalScale = transform.localScale;

        if (PlayerPrefs.GetInt("ButtonA", 0) == 1 &&
            PlayerPrefs.GetInt("ButtonB", 0) == 1 &&
            PlayerPrefs.GetInt("ButtonC", 0) == 1)
        {
            canDash = true;
        }
    }

    private void Update()
    {
        if (isDashing) return;

        float moveInput = Input.GetAxis("Horizontal");
        rb.velocity = new Vector2(moveInput * moveSpeed, rb.velocity.y);

        anim.SetFloat("Speed", Mathf.Abs(moveInput));

        if (moveInput > 0)
            transform.localScale = new Vector3(originalScale.x, originalScale.y, originalScale.z);
        else if (moveInput < 0)
            transform.localScale = new Vector3(-originalScale.x, originalScale.y, originalScale.z);

        isGrounded = Physics2D.Raycast(groundCheck.position, Vector2.down, groundCheckDistance, groundLayer);
        isGrounded2 = Physics2D.Raycast(groundCheck2.position, Vector2.down, groundCheckDistance, groundLayer);
        isGrounded3 = Physics2D.Raycast(groundCheck3.position, Vector2.down, groundCheckDistance, groundLayer);

        bool onGround = isGrounded || isGrounded2 || isGrounded3;
        anim.SetBool("isGrounded", onGround);

        if (onGround && rb.velocity.y <= 0)
        {
            jumpsRemaining = maxJumps;
            anim.SetBool("isJumping", false);

            if (rb.velocity.y < 0)
            {
                PlayGroundSound();
            }
        }

        if (Input.GetKeyDown(KeyCode.Space) && jumpsRemaining > 0)
        {
            anim.SetBool("isJumping", true);
            anim.SetBool("isGrounded", false);
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            jumpsRemaining--;
        }

        if (Input.GetKeyDown(KeyCode.F))
        {
            Lur_HUBManager.Instance.IntentarCurarse();
        }

        // DASH con tecla C
        if (canDash && Input.GetKeyDown(KeyCode.C) && Time.time >= lastDashTime + dashCooldown)
        {
            StartCoroutine(Dash());
        }
    }

    private IEnumerator Dash()
    {
        isDashing = true;
        lastDashTime = Time.time;
        anim.SetTrigger("Dash"); // Opcional: Agregar animación de dash

        Vector2 dashDirection = new Vector2(transform.localScale.x, 0).normalized;
        rb.velocity = dashDirection * dashSpeed;

        yield return new WaitForSeconds(dashDuration);

        isDashing = false;
    }

    public void EnableDash()
    {
        canDash = true;
    }

    public void ActivarAnimacionCuracion()
    {
        anim.SetTrigger("Healing");
        StartCoroutine(BloquearMovimiento(2f));
    }

    public void PlayWalkSound()
    {
        if (SoundController.Instance != null)
        {
            SoundController.Instance.PlaySound("Walk");
        }
    }

    public void PlayJumpSound()
    {
        if (SoundController.Instance != null)
        {
            SoundController.Instance.PlaySound("Jump");
        }
    }

    public void PlayGroundSound()
    {
        if (SoundController.Instance != null)
        {
            SoundController.Instance.PlaySound("Ground");
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Coleccionable"))
        {
            anim.SetTrigger("PickUp");
            rb.velocity = Vector2.zero;
            rb.constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezeRotation;
            enabled = false;
            StartCoroutine(ReactivarMovimiento());
        }

        if (other.CompareTag("Torre"))
        {
            puedeCurarse = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Torre"))
        {
            puedeCurarse = false;
        }
    }

    private IEnumerator ReactivarMovimiento()
    {
        yield return new WaitForSeconds(1.3f);
        rb.constraints = RigidbodyConstraints2D.FreezeRotation;
        enabled = true;
    }

    private IEnumerator BloquearMovimiento(float duracion)
    {
        rb.velocity = Vector2.zero;
        rb.constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezeRotation;
        enabled = false;

        yield return new WaitForSeconds(duracion);

        rb.constraints = RigidbodyConstraints2D.FreezeRotation;
        enabled = true;
    }

    private void OnDrawGizmos()
    {
        if (groundCheck != null && groundCheck2 != null && groundCheck3 != null)
        {
            Gizmos.color = Color.green;
            Gizmos.DrawLine(groundCheck.position, groundCheck.position + Vector3.down * groundCheckDistance);
            Gizmos.DrawLine(groundCheck2.position, groundCheck2.position + Vector3.down * groundCheckDistance);
            Gizmos.DrawLine(groundCheck3.position, groundCheck3.position + Vector3.down * groundCheckDistance);
        }
    }
}
