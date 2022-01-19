using System;
using UnityEngine;

[RequireComponent(typeof(PlayerMovement))]
public class Player : Pawn, IHealth
{
    public event Action<Damage.Type> OnDie;
    public event Action OnHealthChanged;
    public event Action<Bonus> OnGetBonus;

    public PlayerMovement PlayerMovement { get; private set; }
    public PlayerCamera PlayerCamera { get; private set; }
    public PlayerState PlayerState { get; private set; }

    public int Health { get; private set; }
    public int MaxHealth { get => _maxHealth; set => _maxHealth = _maxHealth; }

    [Tooltip("Rotation to forward direction of camera.")]
    [SerializeField] private float _rotationSpeed = 8f;

    [Space]
    [SerializeField] private int _defaultHealth = 3;
    [SerializeField] private int _maxHealth = 4;

    [Space]
    [SerializeField] private Transform _playerModelRoot;

    [Space]
    [SerializeField] private GameObject _landEffectPrefab;


    private void Awake()
    {
        PlayerMovement = GetComponent<PlayerMovement>();
        PlayerCamera = GameObject.FindGameObjectWithTag(Constants.Tags.MAIN_CAMERA).GetComponent<PlayerCamera>();
        PlayerState = GetComponent<PlayerState>();

        //Debug
        var pawncontroller = FindObjectOfType<PawnController>();//
        if (pawncontroller != null)//
        {//
            pawncontroller.SetControlledPawn(this);//
        }//
        //Debug
    }

    private void Start()
    {
        Health = _defaultHealth;

        PlayerMovement.OnLand += OnLand;
    }

    private void Update()
    {
        if(PlayerMovement.IsMovingHorizontal)
        {
            var lookRotation = Quaternion.LookRotation(PlayerMovement.DirectionTransform.forward);
            _playerModelRoot.rotation = Quaternion.Lerp(_playerModelRoot.rotation, lookRotation, Time.deltaTime * _rotationSpeed);
        }
    }

    public override void OnMoveInput(Vector2 input)
    {
        PlayerMovement.SetInput(input);
    }

    public override void OnInput()
    {
        if(Input.GetButtonDown("Jump"))
        {
            PlayerMovement.Jump();
        }

        PlayerCamera.OnInput();
    }

    public void ApplyModifier(Modifier modifier)
    {
        modifier.Apply(this);

        if(modifier is BonusModifier)
        {
            OnGetBonus?.Invoke((modifier as BonusModifier).Bonus);
        }
    }

    public void ApplyModifiers(Modifier[] modifiers)
    {
        foreach (var modifier in modifiers)
        {
            ApplyModifier(modifier);
        }
    }

    public void AddHealth(int hp)
    {
        Health += hp;

        Health = Mathf.Clamp(Health, 0, _maxHealth);

        OnHealthChanged?.Invoke();
    }

    public void TakeDamage(Damage damage)
    {
        Health -= damage.Value;

        Health = Mathf.Clamp(Health, 0, _maxHealth);

        OnHealthChanged?.Invoke();

        if (Health == 0)
        {
            Death(damage.DamageType);
        }
    }

    public int GetHealth()
    {
        return Health;
    }

    private void Death(Damage.Type type)
    {
        OnDie?.Invoke(type);

        Destroy(gameObject);
    }

    private void OnLand()
    {
        var effect = Instantiate(_landEffectPrefab, transform.position - (Vector3.up * 0.25f), Quaternion.identity);

        Destroy(effect, 1.6f);
    }

    private void OnDisable()
    {
        PlayerMovement.OnLand -= OnLand;
    }

    private void OnDestroy()
    {
        PlayerMovement.OnLand -= OnLand;
    }
}
