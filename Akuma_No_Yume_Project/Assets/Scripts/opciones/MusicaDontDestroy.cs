using UnityEngine;

public class AudioManager : MonoBehaviour
{
    private static AudioManager instance;

    private void Awake()
    {
        // Asegurarse de que solo exista un AudioManager en toda la aplicaci�n
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;
        DontDestroyOnLoad(gameObject);
    }

    // M�todo para ajustar el volumen del AudioSource global
    public void SetVolume(float volume)
    {
        AudioListener.volume = volume; // Ajusta el volumen global
    }
}

