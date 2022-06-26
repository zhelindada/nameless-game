using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player
{

    [SerializeField] private PlayerMovement _movement;
    [SerializeField] private PlayerInteraction _interaction;
    [SerializeField] private PlayerUse _useScript;

    private float _health;
    public float Health
    {
        get { return _health; }
        private set
        {
            _health = value;
            if (_health <= 0)
            {
            }
        }
    }

    public void TakeDamage(float damage)
    {
        Health -= damage;
    }
}