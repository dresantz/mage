using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScoreAlocator : MonoBehaviour
{
    // Esse código é colocado no onDied do Enemy, e por isso ele sabe quando o inimigo morre
    [SerializeField]
    private int killScore;

    private ScoreController scoreController;

    private void Awake()
    {
        scoreController = FindObjectOfType<ScoreController>();
    }

    // Esse é o método que está criando valores para ser colocados no amount
    public void AllocateScore()
    {
        scoreController.AddScore(killScore);
    }
}
