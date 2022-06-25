using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerUse : MonoBehaviour
{

    private PlayerMovement _movement;

    private void Awake()
    {
        if (_movement == null)
            _movement = GetComponent<PlayerMovement>();
    }

    public void UseItem() {
        InventoryManager.Instance.UseItem(gameObject.transform.position, _movement.facingDirection);
    }
}
