using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Animator))]
public class Button_Map : MonoBehaviour
{
    private Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        animator.updateMode = AnimatorUpdateMode.UnscaledTime; // Permitir animaciones sin afectar el Time.timeScale
    }
}
