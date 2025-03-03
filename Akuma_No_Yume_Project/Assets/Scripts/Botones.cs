using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Botones : MonoBehaviour
{
    private Button button;

    void Awake()
    {
        button = GetComponent<Button>();
    }

    void Update()
    {
        if (button != null && !button.interactable)
        {
            button.interactable = true;
        }
    }
}
