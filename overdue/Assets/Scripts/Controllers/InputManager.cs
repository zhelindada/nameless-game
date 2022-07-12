using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{

    // Key binds
    [SerializeField] private KeyCode _skillKey;
    [SerializeField] private KeyCode _burstKey;

    [SerializeField] private KeyCode _moveUp;
    [SerializeField] private KeyCode _moveDown;
    [SerializeField] private KeyCode _moveLeft;
    [SerializeField] private KeyCode _moveRight;

    // player script references
    [SerializeField] private Camera _camera;
    [SerializeField] private PlayerInteraction _playerInteraction;
    [SerializeField] private PlayerMovement _playerMovement;
    [SerializeField] private PlayerUse _playerUse;
    [SerializeField] private PlayerAbility _playerAbility;

    private void Update()
    {

        // handling inventory
        if (_playerUse != null)
        {
            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
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
            if (Input.GetMouseButtonDown(0))
            {
                _playerUse.UseItem();
            }
        }

        // handling player interaction
        if (_playerInteraction != null)
        {
            if (Input.GetKeyDown(KeyCode.Space))
                _playerInteraction.Interact();
        }

        if (_playerMovement != null)
        {
            // handling player movement
            _playerMovement.UpdateDirection(_camera.ScreenToWorldPoint(Input.mousePosition) - _playerMovement.transform.position);
            _playerMovement.OnMove(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        }


        if(_playerAbility != null && _playerMovement != null)
        {
            if (Input.GetKeyUp(_skillKey)) {
                _playerAbility.UseSkill(_playerMovement.facingDirection);
            }
            if (Input.GetKeyUp(_burstKey)) {
                _playerAbility.UseBurst(_playerMovement.facingDirection);
            }
        }
    }
}
