using System.Collections.Generic;
using UnityEngine;

public class LevelSelection : MonoBehaviour
{
    [SerializeField] private List<SelectLevelButton> _buttons;

    private void Start()
    {
        for (int i = 0; i < _buttons.Count; i++)
        {
            _buttons[i].SetLevelData(LevelManager.LevelsData[i]);
        }
    }
}