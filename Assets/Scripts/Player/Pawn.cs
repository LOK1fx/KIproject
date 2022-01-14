using UnityEngine;

public abstract class Pawn : MonoBehaviour, IPawnInput
{
    public PawnController PawnController { get; protected set; }

    public void SetPawnController(PawnController controller)
    {
        PawnController = controller;
    }

    public abstract void OnMoveInput(Vector2 input);
    public abstract void OnInput();

    public bool TryGetPawnController(out PawnController controller)
    {
        if(PawnController != null)
        {
            controller = PawnController;

            return true;
        }
        else
        {
            controller = null;

            return false;
        }
    }
}