using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class SelectLevelButton : MonoBehaviour
{
    [SerializeField] private Image _image;
    [SerializeField] private TextMeshProUGUI _levelName;

    private LevelData _levelData;


    public void SetLevelData(LevelData levelData)
    {
        _levelData = levelData;

        _image.sprite = levelData.LevelImage;
        _levelName.text = levelData.Name;
    }

    public void OnClick()
    {
        LevelManager.LoadLevel(_levelData);
    }
}