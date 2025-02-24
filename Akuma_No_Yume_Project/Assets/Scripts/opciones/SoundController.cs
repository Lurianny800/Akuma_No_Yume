using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundController : MonoBehaviour
{
    public static SoundController Instance; // Singleton para acceso global

    private AudioSource audioSource;

    [Header("Sonidos del personaje")]
    public AudioClip jumpSound;
    public AudioClip walkSound;
    public AudioClip groundSound;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        audioSource = GetComponent<AudioSource>();

        if (audioSource == null)
        {
            Debug.LogError("No se encontró un AudioSource en SoundController.");
        }
    }

    public void PlaySound(string soundName)
    {
        if (audioSource == null) return;

        AudioClip clipToPlay = null;

        switch (soundName)
        {
            case "Jump":
                clipToPlay = jumpSound;
                break;
            case "Walk":
                clipToPlay = walkSound;
                break;
            case "Ground":
                clipToPlay = groundSound;
                break;
        }

     if (clipToPlay != null)
        {
            audioSource.PlayOneShot(clipToPlay);
        }
    }
}

