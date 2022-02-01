using UnityEngine;

public class PlayerStartWeapons : MonoBehaviour
{
    [SerializeField] private PlayerInventory _inventory;

    private void Start()
    {
        if(_inventory != null)
        {
            //Gives items
        }
    }
}