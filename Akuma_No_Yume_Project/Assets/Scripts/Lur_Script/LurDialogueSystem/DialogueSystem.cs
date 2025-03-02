using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Collections;

public class DialogueSystem : MonoBehaviour
{
    [System.Serializable]
    public class DialogueSet
    {
        public string minigameKey;
        public DialogueLine[] dialogueLines;
    }

    [System.Serializable]
    public class DialogueLine
    {
        public Sprite characterSprite;
        public string text;
    }

    public Image characterImage;
    public TextMeshProUGUI dialogueText;
    public GameObject dialogueBox;
    public DialogueSet[] dialogues;

    private DialogueLine[] currentDialogue;
    private int currentLine = 0;
    private bool isTyping = false;
    private Coroutine typingCoroutine;
    public float typingSpeed = 0.05f;

    void Start()
    {
        dialogueBox.SetActive(false);

        foreach (DialogueSet dialogue in dialogues)
        {
            if (PlayerPrefs.GetInt(dialogue.minigameKey, 0) == 1)
            {
                currentDialogue = dialogue.dialogueLines;
                StartDialogue();
                PlayerPrefs.SetInt(dialogue.minigameKey, 0);
                PlayerPrefs.Save();
                break;
            }
        }
    }

    public void StartDialogue()
    {
        dialogueBox.SetActive(true);
        Time.timeScale = 0; // ⏸️ Pausar el juego
        currentLine = 0;
        typingCoroutine = StartCoroutine(TypeDialogue());
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (isTyping)
            {
                StopCoroutine(typingCoroutine);
                dialogueText.text = currentDialogue[currentLine].text;
                isTyping = false;
            }
            else
            {
                NextDialogue();
            }
        }
    }

    void NextDialogue()
    {
        if (currentLine < currentDialogue.Length - 1)
        {
            currentLine++;
            typingCoroutine = StartCoroutine(TypeDialogue());
        }
        else
        {
            EndDialogue();
        }
    }

    void EndDialogue()
    {
        dialogueBox.SetActive(false);
        Time.timeScale = 1; // ▶️ Reanudar el juego
    }

    IEnumerator TypeDialogue()
    {
        isTyping = true;
        dialogueText.text = "";
        characterImage.sprite = currentDialogue[currentLine].characterSprite;

        foreach (char letter in currentDialogue[currentLine].text.ToCharArray())
        {
            dialogueText.text += letter;
            yield return new WaitForSecondsRealtime(typingSpeed); // ⏳ Para que funcione con Time.timeScale = 0
        }

        isTyping = false;
    }
}