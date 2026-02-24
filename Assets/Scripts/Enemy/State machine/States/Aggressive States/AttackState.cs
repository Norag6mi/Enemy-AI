using UnityEngine;
using UnityEngine.AI;

public class AttackState : BaseState<AggressiveState>
{
    private AgroStateMachine agroMachine;

    public AttackState(AgroStateMachine stateMachine) : base(AggressiveState.Attack)
    {
        agroMachine = stateMachine;
    }

    public override void EnterState() {//agroMachine.Combat.StartAttack();
    }
    public override void ExitState() => agroMachine.Combat.StopAttack();
    public override void UpdateState() 
    {
        // Get the target from the parent machine
        Transform target = agroMachine.ParentMachine.Target;

        // Only attack if we actually have someone to shoot at!
        if (target != null)
        {
            // Face the target
            Vector3 dir = (target.position - agroMachine.transform.position).normalized;
            dir.y = 0;
            agroMachine.transform.rotation = Quaternion.LookRotation(dir);

            // Now it's safe to fire
            agroMachine.Combat.StartAttack();
        }
        else
        {
            // If the target is gone, stop immediately
            agroMachine.Combat.StopAttack();
        } 
    }

    public override AggressiveState GetNextState()
    {
        if (agroMachine.Combat.IsDead()) return AggressiveState.Die;

        return AggressiveState.Attack;
    }
    public override void OnTriggerEnter(Collider other) { }
    public override void OnTriggerStay(Collider other) { }
    public override void OnTriggerExit(Collider other) { }


}