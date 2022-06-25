using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{

    [SerializeField] private PlayerInteraction _playerInteraction;
    [SerializeField] private PlayerMovement _playerMovement;
    [SerializeField] private PlayerUse _playerUse;

    private void Update()
    {

        // handling inventory
        if (Input.GetKeyDown(KeyCode.Alpha1)) { 
            InventoryManager.Instance.SelectItem(0);
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            InventoryManager.Instance.SelectItem(1);
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            InventoryManager.Instance.SelectItem(2);
        }
        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            InventoryManager.Instance.SelectItem(3);
        }

        // handling player use item
        if (Input.GetKeyDown(KeyCode.E)) {
            _playerUse.UseItem();
        }

        // handling player interaction
        if (Input.GetKeyDown(KeyCode.Space))
            _playerInteraction.Interact();

        // handling player movement
        _playerMovement.UpdateDirection();
    }
}
