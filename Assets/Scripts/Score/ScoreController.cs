using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ScoreController : MonoBehaviour
{
    public UnityEvent OnScoreChanged;

public int Score {  get; private set; }

    // o amount � pego do EnemyScoreAlocator, do m�todo AllocateScore
    public void AddScore(int amount)
    {
        Score += amount;
        OnScoreChanged.Invoke();
    }
}
