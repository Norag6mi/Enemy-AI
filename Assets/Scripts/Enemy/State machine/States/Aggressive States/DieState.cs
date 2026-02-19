using UnityEngine;
using UnityEngine.AI;

public class DieState : BaseState<AggressiveState>
{
    private AgroStateMachine agroMachine;

    public DieState(AgroStateMachine stateMachine) : base(AggressiveState.Die)
    {
        agroMachine = stateMachine;
    }

    public override void EnterState()
    {
        agroMachine.Agent.isStopped = true;
        agroMachine.Combat.StopAttack();
        
        // Tells the parent machine to stop working entirely
        if (agroMachine.ParentMachine != null)
            agroMachine.ParentMachine.enabled = false;
    }

    public override void ExitState() { }
    public override void UpdateState() { }

    public override AggressiveState GetNextState() => AggressiveState.Die;

    public override void OnTriggerEnter(Collider other) { }
    public override void OnTriggerStay(Collider other) { }
    public override void OnTriggerExit(Collider other) { }
    
}