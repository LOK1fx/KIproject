using System.Collections.Generic;
using UnityEngine;

public class LevelSelection : MonoBehaviour
{
    [SerializeField] private SelectLevelButton _buttonPrefab;

    [Space]
    [SerializeField] private Transform _levelButtonsParent;

    private List<SelectLevelButton> _buttons = new List<SelectLevelButton>();

    private void Start()
    {
        for (int i = 0; i < LevelManager.LevelsData.Count; i++)
        {
            var button = Instantiate(_buttonPrefab, _levelButtonsParent);

            _buttons.Add(button);
        }

        for (int i = 0; i < _buttons.Count; i++)
        {
            _buttons[i].SetLevelData(LevelManager.LevelsData[i]);
        }
    }
}