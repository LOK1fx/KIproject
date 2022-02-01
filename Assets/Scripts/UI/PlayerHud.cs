using UnityEngine;
using TMPro;

[RequireComponent(typeof(CanvasGroup))]
public class PlayerHud : MonoBehaviour
{
    public float HudAlpha = 1f;

    [SerializeField] private TextMeshProUGUI _hpText;
    [SerializeField] private TextMeshProUGUI _scoreText;

    [Space]
    [Header("Components")]
    [SerializeField] private BonusTakeOverlay _onBonusTakenScreen;
    [SerializeField] private DeathScreenUI _deathScreen;
    [SerializeField] private PauseMenu _pauseMenu;

    private PlayerController _playerController;
    private Player _player;

    private CanvasGroup _canvas;

    private void Awake()
    {
        _canvas = GetComponent<CanvasGroup>();
        _canvas.alpha = 0f;

        _playerController = FindObjectOfType<PlayerController>();
        _playerController.OnControlledPawnChanged += OnControlledPawnChanged;
    }

    private void OnControlledPawnChanged(Pawn obj)
    {
        if(_playerController.TryGetControlledPawn<Player>(out var player))
        {
            _player = player;
        }
        else
        {
            Debug.Log("ControlledPawn isn't player!");

            return;
        }

        _player.OnDie += OnPlayerDie;
        _player.OnHealthChanged += OnPlayerHpChanged;
        _player.OnGetBonus += OnPlayerGetBonus;

        _playerController.OnScoreUpdated += OnScoreUpdated;
        _playerController.InputPause += OnPause;

        Debug.Log(name + " | " + _player + " | " + _playerController);
    }

    private void OnPause()
    {
        if(_pauseMenu.IsOpen)
        {
            _pauseMenu.ReturnToGame();
        }
        else
        {
            _pauseMenu.ShowMenu();
        }
    }

    private void Update()
    {
        _canvas.alpha = Mathf.Lerp(_canvas.alpha, HudAlpha, Time.deltaTime * 8f);
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

    private void OnPlayerDie(Damage damage)
    {
        _hpText.text = $"Player is dead";

        _deathScreen.Show(damage);
    }
}