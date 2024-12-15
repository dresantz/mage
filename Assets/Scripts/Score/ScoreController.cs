using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ScoreController : MonoBehaviour
{
    public UnityEvent OnScoreChanged;

public int Score {  get; private set; }

    // o amount é pego do EnemyScoreAlocator, do método AllocateScore
    public void AddScore(int amount)
    {
        Score += amount;
        OnScoreChanged.Invoke();
    }
}
