using System;
using UnityEngine;

public class PlayerController : PawnController
{
    public event Action OnScoreUpdated;
    public event Action InputPause;

    public int Score { get; private set; }

    protected override void Update()
    {
        base.Update();

        if(Input.GetKeyDown(KeyCode.Escape))
        {
            InputPause?.Invoke();
        }
    }

    public void AddScore(int value)
    {
        Score += value;

        OnScoreUpdated?.Invoke();
    }
}