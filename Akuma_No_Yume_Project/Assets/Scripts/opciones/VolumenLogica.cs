using UnityEngine;

public class AudioManager2 : MonoBehaviour
{
    private static AudioManager2 instance;

    private void Awake()
    {
        // Asegurarse de que solo exista un AudioManager en toda la aplicación
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;
        DontDestroyOnLoad(gameObject);
    }

    // Método para ajustar el volumen del AudioSource global
    public void SetVolume(float volume)
    {
        AudioListener.volume = volume; // Ajusta el volumen global
    }
}


