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

    public bool TryGetPawnController<T>(out T controller) where T : PawnController
    {
        if(PawnController != null && PawnController is T)
        {
            controller = (T)PawnController;

            return true;
        }
        else
        {
            controller = null;

            return false;
        }
    }
}