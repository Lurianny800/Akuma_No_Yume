using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FigurasSeleccionable : MonoBehaviour
{
    private SelectionManager selectionManager;

    void Start()
    {
        selectionManager = FindObjectOfType<SelectionManager>();
    }

    private void OnMouseDown()
    {
        if (transform.parent == selectionManager.panelDestino)
        {
            selectionManager.ultimaFiguraEnDestino = gameObject;
        }
    }
}
