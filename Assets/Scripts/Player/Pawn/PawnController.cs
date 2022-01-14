using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PawnController : MonoBehaviour
{
    public Pawn ControlledPawn { get; private set; }

    protected virtual void Update()
    {
        if(ControlledPawn)
        {
            ControlledPawn.OnInput();

            var moveInput = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));

            ControlledPawn.OnMoveInput(moveInput);
        }  
    }

    public void SetControlledPawn(Pawn pawn)
    {
        ControlledPawn = pawn;
        ControlledPawn.SetPawnController(this);
    }
}