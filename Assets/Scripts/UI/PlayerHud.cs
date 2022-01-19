using UnityEngine;
using TMPro;

public class PlayerHud : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _hpText;
    [SerializeField] private TextMeshProUGUI _scoreText;

    [Space]
    [SerializeField] private BonusTakeOverlay _onBonusTakenScreen;

    private PlayerController _playerController;
    private Player _player;

    private void Start()
    {
        _playerController = FindObjectOfType<PlayerController>();

        _player = _playerController.GetControlledPawn<Player>();

        if(_player && _playerController)
        {
            _player.OnDie += OnPlayerDie;
            _player.OnHealthChanged += OnPlayerHpChanged;
            _player.OnGetBonus += OnPlayerGetBonus;

            _playerController.OnScoreUpdated += OnScoreUpdated;
        }

        Debug.Log(name + " | " + _player + " | " + _playerController);
    }

    private void OnPlayerGetBonus(Bonus bonus)
    {
        _onBonusTakenScreen.OnBonusTaken(bonus);
    }

    private void OnScoreUpdated()
    {
        _scoreText.text = $"Score: {_playerController.Score}";
    }

    private void OnPlayerHpChanged()
    {
        _hpText.text = $"HP: {_player.Health}";
    }

    private void OnPlayerDie(Damage.Type type)
    {
        _hpText.text = $"Player is dead";
    }
}