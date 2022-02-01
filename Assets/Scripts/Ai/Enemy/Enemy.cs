using UnityEngine;

public class Enemy : Pawn, IHealth
{
    public int Health { get; private set; }

    [SerializeField] private int _defaultHp = 3;

    public PlayerMovement EnemyMovement { get; private set; }
    public PlayerState EnemyState { get; private set; }

    private Pawn _targetPawn;

    private void Start()
    {
        PawnController.Instance.OnControlledPawnChanged += OnPawnControllerPawnChanged;

        EnemyMovement = GetComponent<PlayerMovement>();
        EnemyState = GetComponent<PlayerState>();

        Health = _defaultHp;
    }

    private void Update()
    {
        //test
        if(_targetPawn != null)
        {
            var input = (_targetPawn.transform.position - transform.position).normalized;

            EnemyMovement.SetInput(new Vector2(0, 1f));
            EnemyMovement.DirectionTransform.rotation = Quaternion.LookRotation(input);
        }
    }

    public override void OnInput()
    {

    }

    public override void OnMoveInput(Vector2 input)
    {

    }

    public override void OnPocces()
    {
        
    }

    public void AddHealth(int hp)
    {
        Health += hp;
    }

    public int GetHealth()
    {
        return Health;
    }

    public void TakeDamage(Damage damage)
    {
        Health -= damage.Value;

        if(Health <= 0)
        {
            Death(damage);
        }
    }

    private void Death(Damage damageData)
    {
        Destroy(gameObject);
    }

    private void OnPawnControllerPawnChanged(Pawn obj)
    {
        if (PawnController.Instance.TryGetControlledPawn<Player>(out var player))
        {
            if (player.InPlayerControl)
            {
                _targetPawn = player;
            }
        }
    }

    private void OnDrawGizmos()
    {
        if(_targetPawn != null)
        {
            var dir = (_targetPawn.transform.position - transform.position).normalized;

            Gizmos.color = Color.red;
            Gizmos.DrawRay(transform.position, dir);
        }
    }
}