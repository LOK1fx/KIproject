using System;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerMovement))]
public class Player : Pawn, IHealth
{
    public event Action<Damage> OnDie;
    public event Action OnHealthChanged;
    public event Action<Bonus> OnGetBonus;

    public PlayerMovement PlayerMovement { get; private set; }
    public PlayerCamera PlayerCamera { get; private set; }
    public PlayerState PlayerState { get; private set; }

    public List<Modifier> ActiveModifiers { get; private set; }

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

        ActiveModifiers = new List<Modifier>();
    }

    private void Start()
    {
        Health = _defaultHealth;

        PlayerMovement.OnLand += OnLand;
    }

    private void Update()
    {
        RotatePlayer();
    }

    private void RotatePlayer()
    {
        if (PlayerMovement.IsMovingHorizontal && PlayerMovement.InputDirection != Vector3.zero)
        {
            var lookRotation = Quaternion.LookRotation(PlayerMovement.InputDirection);

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

        ActiveModifiers.Add(modifier);
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
        foreach (var modifier in ActiveModifiers)
        {
            if(modifier is BonusModifier)
            {
                if((modifier as BonusModifier).MakePlayerInvicible == true)
                {
                    if(damage.DamageType == Damage.Type.Void)
                    {
                        break;
                    }
                    else
                    {
                        return;
                    }
                }
            }
        }

        Health -= damage.Value;

        Health = Mathf.Clamp(Health, 0, _maxHealth);

        OnHealthChanged?.Invoke();

        if (Health == 0)
        {
            Death(damage);
        }
    }

    public int GetHealth()
    {
        return Health;
    }

    private void Death(Damage damage)
    {
        OnDie?.Invoke(damage);

        Destroy(gameObject);
    }

    private void OnLand(Vector3 velocity)
    {
        if(velocity.y >= -7f)
        {
            var effect = Instantiate(_landEffectPrefab, transform.position - (Vector3.up * 0.25f), Quaternion.identity);

            Destroy(effect, 1.6f);
        } 
    }

    private void OnDisable()
    {
        PlayerMovement.OnLand -= OnLand;
    }

    private void OnDestroy()
    {
        PlayerMovement.OnLand -= OnLand;
    }

    public override void OnPocces()
    {
        PlayerCamera.Player = this;
    }
}
