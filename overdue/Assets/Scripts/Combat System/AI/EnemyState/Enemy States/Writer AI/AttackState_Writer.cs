using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackState_Writer : State<WriterEntity>
{
    private static AttackState_Writer _Instance;
    private AttackState_Writer()
    {
        if (_Instance != null)
            return;
        _Instance = this;
    }

    public static State<WriterEntity> Instance
    {
        get
        {
            if (_Instance == null)
                _Instance = new AttackState_Writer();
            return _Instance;
        }
    }
    public override void EnterState(WriterEntity owner)
    {
        Debug.Log("Writer Entered Attack State");
    }

    public override void ExitState(WriterEntity _owner)
    {
        Debug.Log("Writer Exited Attack State");
    }

    public override void UpdateState(WriterEntity _owner)
    {
        if (!_owner.AI.playerInAttackRange)
            _owner.AI.FiniteStateMachine.ChangeState(ApproachState_Writer.Instance);
    }

}
