using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanelManager : MonoBehaviour
{
    void Start()
    {
        // Obtiene el nombre del panel objetivo de PlayerPrefs
        string targetPanelName = PlayerPrefs.GetString("TargetPanel", "");

        if (!string.IsNullOrEmpty(targetPanelName))
        {
            Transform targetPanel = transform.Find(targetPanelName);

            if (targetPanel != null)
            {
                // Desactiva todos los paneles hijos
                foreach (Transform child in transform)
                {
                    child.gameObject.SetActive(false);
                }

                // Activa el panel objetivo
                targetPanel.gameObject.SetActive(true);
            }
            else
            {
                Debug.LogWarning($"No se encontró el panel '{targetPanelName}' en el Canvas.");
            }
        }
    }
}
