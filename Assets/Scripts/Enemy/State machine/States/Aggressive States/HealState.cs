using UnityEngine;
using UnityEngine.AI;

public class HealState : BaseState<AggressiveState>
{
    private AgroStateMachine agroMachine;

    public HealState(AgroStateMachine stateMachine) : base(AggressiveState.Heal)
    {
        agroMachine = stateMachine;
    }

    public override void EnterState()
    {
        agroMachine.Combat.StartHeal(50,5.0f); 
    }

    public override void ExitState() { }
    public override void UpdateState() { }

    public override AggressiveState GetNextState()
    {
        if (agroMachine.Combat.IsDead()) return AggressiveState.Die;

        // Logic here to return to Attack after healing is done
       
        return AggressiveState.Heal; 
    }

    public override void OnTriggerEnter(Collider other) { }
    public override void OnTriggerStay(Collider other) { }
    public override void OnTriggerExit(Collider other) { }

}