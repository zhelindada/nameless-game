using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState_Writer : State<WriterEntity>
{
    private static IdleState_Writer _Instance;
    private IdleState_Writer()
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
                _Instance = new IdleState_Writer();
            return _Instance;
        }
    }
    public override void EnterState(WriterEntity owner)
    {
        Debug.Log("Writer Entered Idle State");
    }
    public override void ExitState(WriterEntity _owner)
    {
        Debug.Log("Writer Exited Idle State");
    }

    public override void UpdateState(WriterEntity _owner)
    {
        if (_owner.AI.playerInDetectionRange)
            _owner.AI.FiniteStateMachine.ChangeState(ApproachState_Writer.Instance);
        
    }
}
