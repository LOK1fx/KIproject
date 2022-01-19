using UnityEngine;

public class PawnController : MonoBehaviour
{
    public PawnController Instance { get; set; }

    public Pawn ControlledPawn { get; private set; }

    private void Awake()
    {
        Instance = this;
    }

    protected virtual void Update()
    {
        if(ControlledPawn)
        {
            ControlledPawn.OnInput();

            var moveInput = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));

            ControlledPawn.OnMoveInput(moveInput);
        }  
    }

    public T GetControlledPawn<T>() where T: Pawn
    {
        if((T)ControlledPawn)
        {
            return (T)ControlledPawn;
        }
        else
        {
            return null;
        }
    }

    public void SetControlledPawn(Pawn pawn)
    {
        ControlledPawn = pawn;
        ControlledPawn.SetPawnController(this);
    }
}