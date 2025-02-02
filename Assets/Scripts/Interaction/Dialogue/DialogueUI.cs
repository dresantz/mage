using TMPro;
using UnityEngine;
using System.Collections;

public class DialogueUI : MonoBehaviour
{
    public static DialogueUI Instance; // Singleton para f�cil acesso
    public RectTransform dialogBox; // Refer�ncia ao painel da UI
    public TextMeshProUGUI dialogueText;

    private Transform speakerTransform; // Quem est� falando (NPC ou Player)
    public Vector3 offset = new Vector3(0, 2f, 0); // Ajuste a altura da caixa de di�logo

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
            // Atualiza a posi��o da caixa de di�logo conforme o falante se move
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
