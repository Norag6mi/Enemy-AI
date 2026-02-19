using UnityEngine;
using UnityEngine.AI;

public class ReloadState : BaseState<AggressiveState>
{
    private AgroStateMachine agroMachine;

    public ReloadState(AgroStateMachine stateMachine) : base(AggressiveState.Reload)
    {
        agroMachine = stateMachine;
    }

    public override void EnterState() => agroMachine.Combat.Reload();
    public override void ExitState(){}
    public override void UpdateState() { }

    public override AggressiveState GetNextState()
    {
        if (agroMachine.Combat.IsDead()) return AggressiveState.Die;

        //  Transition: Once reload is finished, go back to Attack
        if (!agroMachine.Combat.IsReloading()) return AggressiveState.Attack;

        return AggressiveState.Reload;
    }

    public override void OnTriggerEnter(Collider other) { }
    public override void OnTriggerStay(Collider other) { }
    public override void OnTriggerExit(Collider other) { }

}