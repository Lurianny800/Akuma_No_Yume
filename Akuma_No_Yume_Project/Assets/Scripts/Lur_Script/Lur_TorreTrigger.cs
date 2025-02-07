using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lur_TorreTrigger : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Lur_MapPanelManager mapPanel = FindObjectOfType<Lur_MapPanelManager>();
            if (mapPanel != null)
            {
                mapPanel.SetCercaDeTorre(true);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Lur_MapPanelManager mapPanel = FindObjectOfType<Lur_MapPanelManager>();
            if (mapPanel != null)
            {
                mapPanel.SetCercaDeTorre(false);
            }
        }
    }
}
