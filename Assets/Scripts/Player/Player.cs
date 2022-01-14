using UnityEngine;

[RequireComponent(typeof(PlayerMovement))]
public class Player : Pawn, IHealth
{
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
    }

    private void Start()
    {
        //Debug
        FindObjectOfType<PawnController>().SetControlledPawn(this);

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

    public void AddHealth(int hp)
    {
        Health += hp;

        Health = Mathf.Clamp(Health, 0, _maxHealth);
    }

    public void TakeDamage(int hp)
    {
        Health -= hp;

        Health = Mathf.Clamp(Health, 0, _maxHealth);
    }

    public int GetHealth()
    {
        return Health;
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
