using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

[RequireComponent(typeof(CanvasGroup))]
public class DeathScreenUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _titleText;
    [SerializeField] private Image _killerAvatar;

    private CanvasGroup _canvasGroup;

    private float _targetAlpha;

    private void Awake()
    {
        _canvasGroup = GetComponent<CanvasGroup>();
        _canvasGroup.alpha = 0f;

        _targetAlpha = 0f;
    }

    private void Update()
    {
        _canvasGroup.alpha = Mathf.Lerp(_canvasGroup.alpha, _targetAlpha, Time.deltaTime * 5f);
    }

    public void Show(Damage damageData)
    {
        if(damageData.Sender == null)
        {
            return;
        }

        _targetAlpha = 1f;

        _titleText.text = $"You've been killed by {damageData.Sender.Name}";
        _killerAvatar.sprite = damageData.Sender.ActorImage;
    }
}