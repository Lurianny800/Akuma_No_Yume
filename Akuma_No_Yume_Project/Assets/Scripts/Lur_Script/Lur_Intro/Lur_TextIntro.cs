using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Lur_TextIntro : MonoBehaviour
{
    public TextMeshProUGUI dialogueText;
    public float typingSpeed = 0.05f; // Velocidad del efecto typing
    public string nextSceneName; // Nombre de la siguiente escena

    private int currentDialogueIndex = 0;
    private bool isTyping = false;
    private bool canAdvance = false; // Controla si se puede pasar de diálogo

    private string[] dialogues = new string[]
    {
        "Un día como cualquier otro, Hikari, una chica de 16 años, se encontraba sola en casa.",
        "Pasó horas frente a la pantalla de su ordenador, viendo su serie animada favorita, perdiendo la noción del tiempo.",
        "No fue hasta que el hambre se hizo insoportable que, con cierta pereza, decidió levantarse."
    };

    void Start()
    {
        StartCoroutine(TypeText(dialogues[currentDialogueIndex]));
    }

    void Update()
    {
        if (Input.anyKeyDown && canAdvance)
        {
            currentDialogueIndex++;

            if (currentDialogueIndex < dialogues.Length)
            {
                StartCoroutine(TypeText(dialogues[currentDialogueIndex]));
            }
            else
            {
                SceneManager.LoadScene(nextSceneName); // Cambia de escena
            }
        }
    }

    IEnumerator TypeText(string text)
    {
        isTyping = true;
        canAdvance = false;
        dialogueText.text = "";

        foreach (char letter in text.ToCharArray())
        {
            dialogueText.text += letter;
            yield return new WaitForSeconds(typingSpeed);
        }

        isTyping = false;
        canAdvance = true; // Habilita avanzar con una tecla
    }
}
