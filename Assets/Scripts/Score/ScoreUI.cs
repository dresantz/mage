using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreUI : MonoBehaviour
{
    private TMP_Text scoreText;

    private void Awake()
    {
        scoreText = GetComponent<TMP_Text>();
    }

    public void UpdateScore (ScoreController scoreController)
    {
        // O $ antes de string serve para adicionar valores dentro das chaves
        // O valor do Score é colocado no texto
        scoreText.text = $"Score: {scoreController.Score}";
    }
}
