using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WriterEntity : Entity
{
    [SerializeField] private GameObject go;
    [SerializeField] private Writer writer;

    [SerializeField] private GameObject _attackObject;
    [SerializeField] private float _attackObjectSpawnDistance;
    [SerializeField] private float _detectionRange;
    [SerializeField] private float _attackRange;
    [SerializeField] private float _speed;
    [SerializeField] private float _attackCooldown;

    //properties
    [SerializeField] public float Health { 
        get { return health; } 
        private set {
            health = value;
            // event methods
            writer.UpdateHealthBar(health / maxHealth);
        }
    }

    //debug
    [SerializeField] private string _state;

    private float currentAttackCooldown = 0;

    public AI_Writer AI;

    protected new void Awake()
    {
        base.Awake();

        go = gameObject;
        writer = GetComponent<Writer>();
        
        if (AI == null)
            AI = new AI_Writer(this, IdleState_Writer.Instance);
        
    }
    private void Update()
    {
        base.Update();
        currentAttackCooldown -= Time.deltaTime;

        UpdateLogicVariables();
        AI.Update();

        // updating debug values
        _state = AI.FiniteStateMachine.currentState.ToString();
    }
    private void UpdateLogicVariables()
    {
        GameObject go = GameObject.Find("Player");
        if(go == null) 
        {
            AI.playerInDetectionRange = false;
            return;
        }

        Transform t = go.transform;
        AI.playerInDetectionRange = (t.position - transform.position).magnitude < _detectionRange;
        AI.playerInAttackRange = (t.position - transform.position).magnitude < _attackRange;
        AI.attackOnCooldown = currentAttackCooldown > 0;
    }
    public override void Die()
    {
        Debug.Log("Writer dies");
        writer.DropStuff();
        Destroy(gameObject);
    }
    public override void TakeDamage(float dmg)
    {
        Debug.Log("Writer took some damage");
        Health -= dmg;
        if (Health <= 0) {
            Die();
        }
    }
    public override void MoveTowards(Vector2 target)
    {
        /*Debug.Log("Writer is moving towards " + target);*/

        Vector2 moveVec = new Vector2(target.x - transform.position.x, target.y - transform.position.y).normalized * Time.deltaTime * _speed;
        Vector3 newLoc = transform.position + new Vector3(moveVec.x, moveVec.y);
        transform.position = newLoc;
        
    }

    public override void Attack(Vector2 target)
    {
        Debug.Log("Writer is attacking " + target);

        currentAttackCooldown = _attackCooldown;

        GameObject damageInstance = Instantiate(_attackObject);


        Vector2 temp = target - new Vector2(gameObject.transform.position.x, gameObject.transform.position.y);

        // setting the rotation
        float angle = Mathf.Atan2(temp.y, temp.x);
        damageInstance.transform.Rotate(0, 0, angle);

        //setting the position
        Vector2 newPos = transform.position;
        newPos += _attackObjectSpawnDistance * temp.normalized;
        damageInstance.transform.position = newPos;
    }

    public override void Attack(Entity target)
    {
        Debug.Log("Writer is attacking " + target);

        currentAttackCooldown = _attackCooldown;
        Attack(target.transform.position);
    }
    public override void GetHitBy(DamageCollider dmg)
    {
        base.GetHitBy(dmg);
    }
    public void MoveTowardsPlayer()
    {
        Debug.Log("Writer moves towards player");
        Transform t = GameObject.Find("Player").transform;
        MoveTowards(t.position);
    }

    public void AttackPlayer() {
        Transform t = GameObject.Find("Player").transform;
        Attack(t.position);
    }




}
