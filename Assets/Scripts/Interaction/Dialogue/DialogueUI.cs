using TMPro;
using UnityEngine;
using System.Collections;

public class DialogueUI : MonoBehaviour
{
    public static DialogueUI Instance; // Singleton para fácil acesso
    public RectTransform dialogBox; // Referência ao painel da UI
    public TextMeshProUGUI dialogueText;

    private Transform speakerTransform; // Quem está falando (NPC ou Player)
    public Vector3 offset = new Vector3(0, 2f, 0); // Ajuste a altura da caixa de diálogo

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);

        dialogBox.gameObject.SetActive(false);
    }

    private void Update()
    {
        if (speakerTransform != null)
        {
            // Atualiza a posição da caixa de diálogo conforme o falante se move
            dialogBox.position = Camera.main.WorldToScreenPoint(speakerTransform.position + offset);
        }
    }

    public void ShowDialogue(string text, Transform speaker)
    {
        speakerTransform = speaker;
        dialogueText.text = text;
        dialogBox.gameObject.SetActive(true);
    }

    public void HideDialogue()
    {
        dialogBox.gameObject.SetActive(false);
        speakerTransform = null;
    }
}
