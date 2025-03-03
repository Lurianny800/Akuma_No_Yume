using System.Collections;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Lur_DialogueIntro : MonoBehaviour
{
    [System.Serializable]
    public class DialogueLine
    {
        public Sprite characterSprite;
        public string dialogueText;
    }

    public Image characterImage;
    public TextMeshProUGUI dialogueText;
    public GameObject dialoguePanel;

    public DialogueLine[] dialogue1;
    public DialogueLine[] dialogue2;
    public DialogueLine[] dialogue3;

    public float typingSpeed = 0.05f;
    public GameObject animationPanel;
    public Animator panelAnimator;

    private int currentDialogueIndex = 0;
    private bool isTyping = false;
    private bool canAdvance = false;
    private Coroutine typingCoroutine;
    private int currentDialogueState = 1; // 1 = Diálogo 1, 2 = Diálogo 2, 3 = Diálogo 3
    private bool triggerActivated = false; // Controla que el trigger solo se active una vez
    private bool dialogue3Started = false; // Variable de control

    void Start()
    {
        Time.timeScale = 0f; // Pausa el juego
        dialoguePanel.SetActive(true);
        typingCoroutine = StartCoroutine(TypeText(dialogue1[currentDialogueIndex])); // Comienza el diálogo 1
    }

    void Update()
    {
        if (Input.anyKeyDown)
        {
            if (isTyping)
            {
                StopCoroutine(typingCoroutine);
                dialogueText.text = GetCurrentDialogue()[currentDialogueIndex].dialogueText;
                isTyping = false;
                canAdvance = true;
            }
            else if (canAdvance)
            {
                currentDialogueIndex++;

                if (currentDialogueIndex < GetCurrentDialogue().Length)
                {
                    typingCoroutine = StartCoroutine(TypeText(GetCurrentDialogue()[currentDialogueIndex]));
                }
                else
                {
                    EndDialogue();
                }
            }
        }
    }

    IEnumerator TypeText(DialogueLine line)
    {
        isTyping = true;
        canAdvance = false;
        dialogueText.text = "";
        characterImage.sprite = line.characterSprite;

        foreach (char letter in line.dialogueText.ToCharArray())
        {
            dialogueText.text += letter;
            yield return new WaitForSecondsRealtime(typingSpeed);
        }

        isTyping = false;
        canAdvance = true;
    }

    void EndDialogue()
    {
        dialoguePanel.SetActive(false);
        Time.timeScale = 1f; // Reanuda el juego

        // Ahora solo activamos la animación si el diálogo actual es el 2 (evita que se active tras el diálogo 1)
        if (currentDialogueState == 2)
        {
            StartCoroutine(PlayPanelAnimation());
        }
    }

    private DialogueLine[] GetCurrentDialogue()
    {
        if (currentDialogueState == 2)
            return dialogue2;
        else if (currentDialogueState == 3)
            return dialogue3;
        else
            return dialogue1;
    }

    public void StartDialogue2()
    {
        if (!triggerActivated) // Solo permite activarlo una vez
        {
            triggerActivated = true;
            currentDialogueState = 2;
            currentDialogueIndex = 0;
            dialoguePanel.SetActive(true);
            Time.timeScale = 0f;
            typingCoroutine = StartCoroutine(TypeText(dialogue2[currentDialogueIndex]));
        }
    }

    IEnumerator PlayPanelAnimation()
    {
        if (dialogue3Started) yield break; // Evita que se ejecute más de una vez

        dialogue3Started = true; // Marcamos que ya inició

        animationPanel.SetActive(true);
        panelAnimator.SetTrigger("PlayAnimation");

        yield return new WaitForSeconds(0.1f);

        float animationTime = panelAnimator.GetCurrentAnimatorStateInfo(0).length;
        yield return new WaitForSeconds(animationTime);

        StartDialogue3(); // Se ejecuta solo una vez
    }

    public void StartDialogue3()
    {
        currentDialogueState = 3;
        currentDialogueIndex = 0;
        dialoguePanel.SetActive(true);
        Time.timeScale = 0f;
        typingCoroutine = StartCoroutine(TypeText(dialogue3[currentDialogueIndex]));
    }
}
