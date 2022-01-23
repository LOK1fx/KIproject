using UnityEngine;

[RequireComponent(typeof(Collider))]
public class Finish : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if(other.TryGetComponent<Player>(out var player))
        {
            LevelManager.SetLevelCompleted(LevelManager.GetCurrentLevelData().Name);
            LevelManager.LoadNextLevel();
        }
    }
}