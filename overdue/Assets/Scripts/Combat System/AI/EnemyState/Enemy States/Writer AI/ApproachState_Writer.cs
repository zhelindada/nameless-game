using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ApproachState_Writer : State<WriterEntity>
{
    private static ApproachState_Writer _Instance;
    private ApproachState_Writer()
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
                _Instance = new ApproachState_Writer();
            return _Instance;
        }
    }

    public override void EnterState(WriterEntity owner) {
        Debug.Log("Writer Entered Approach State");
    }
    public override void ExitState(WriterEntity _owner)
    {
        Debug.Log("Writer Exited Approach State");
    }
    public override void UpdateState(WriterEntity _owner)
    {
        State<WriterEntity> changedState = null;
        if (!_owner.AI.playerInDetectionRange)
        {
            changedState = IdleState_Writer.Instance;
        }
        else if (_owner.AI.playerInAttackRange ){
            changedState = AttackState_Writer.Instance;
        }
        if (changedState != null) {
            _owner.AI.FiniteStateMachine.ChangeState(changedState);
            /*Debug.Log(changedState.GetType());*/
        }


    }
}
