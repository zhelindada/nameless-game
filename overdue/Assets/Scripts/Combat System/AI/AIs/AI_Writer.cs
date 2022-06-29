using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI_Writer : AI<WriterEntity>
{

    public bool playerInDetectionRange;
    public bool playerInAttackRange;
    public bool attackOnCooldown = false;

    public AI_Writer(WriterEntity owner, State<WriterEntity> startingState) {
        Initialise(owner, IdleState_Writer.Instance);
    }

    public override void Initialise(WriterEntity owner, State<WriterEntity> startingState)
    {
        base.Initialise(owner, startingState);
    }

    public override void Update()
    {
        Debug.Log("Writer AI updating");
        base.Update();
        if (FiniteStateMachine.currentState == IdleState_Writer.Instance)
        {

        }
        if (FiniteStateMachine.currentState == ApproachState_Writer.Instance)
        {
            Owner.MoveTowardsPlayer();
        }
        if (FiniteStateMachine.currentState == AttackState_Writer.Instance)
        {
            Debug.Log("Attack State");

            if (!attackOnCooldown)
            {
                Debug.Log("Attack plauer");
                Owner.AttackPlayer();
            }
            else { 
                
            }
        }
    }
    
}
