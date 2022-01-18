using System;

public class PlayerController : PawnController
{
    public event Action OnScoreUpdated;

    public int Score { get; private set; }

    public void AddScore(int value)
    {
        Score += value;

        OnScoreUpdated?.Invoke();
    }
}