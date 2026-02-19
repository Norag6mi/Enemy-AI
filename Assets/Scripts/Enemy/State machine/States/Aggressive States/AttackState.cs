using UnityEngine;
using UnityEngine.AI;

public class AttackState : BaseState<AggressiveState>
{
    private AgroStateMachine agroMachine;

    public AttackState(AgroStateMachine stateMachine) : base(AggressiveState.Attack)
    {
        agroMachine = stateMachine;
    }

    public override void EnterState() => agroMachine.Combat.StartAttack();
    public override void ExitState() => agroMachine.Combat.StopAttack();
    public override void UpdateState() { }

    public override AggressiveState GetNextState()
    {
        if (agroMachine.Combat.IsDead()) return AggressiveState.Die;
        
        //  Transition: If out of ammo, go to Reload
        if (agroMachine.Combat.GetCurrentAmmo() <= 0) return AggressiveState.Reload;

        return AggressiveState.Attack;
    }
    public override void OnTriggerEnter(Collider other) { }
    public override void OnTriggerStay(Collider other) { }
    public override void OnTriggerExit(Collider other) { }


}