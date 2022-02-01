using System;
using UnityEngine;

public class PawnController : MonoBehaviour
{
    public event Action<Pawn> OnControlledPawnChanged;

    public static PawnController Instance { get; set; }

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

    public bool TryGetControlledPawn<T>(out T pawn) where T: Pawn
    {
        if((T)ControlledPawn)
        {
            pawn = (T)ControlledPawn;

            return true;
        }
        else
        {
            pawn = null;

            return false;
        }
    }

    public void SetControlledPawn(Pawn pawn, bool player)
    {
        ControlledPawn = pawn;
        ControlledPawn.InPlayerControl = player;
        ControlledPawn.SetPawnController(this);
        ControlledPawn.OnPocces();

        OnControlledPawnChanged?.Invoke(ControlledPawn);
    }
}