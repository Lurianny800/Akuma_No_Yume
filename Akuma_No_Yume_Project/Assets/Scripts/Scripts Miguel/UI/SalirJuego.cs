using UnityEngine;

public class SalirDelJuego : MonoBehaviour
{
    public void Salir()
    {
        // Cierra la aplicaci�n
        Application.Quit();

        // Solo para asegurarse de que funciona en el editor de Unity (no se necesita en el juego final)
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#endif
    }
}
