using UnityEngine;

public class HealState : BaseState<AggressiveState>
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private AgroStateMachine agroMachine;

    // This is the constructor
    public HealState(AgroStateMachine stateMachine) : base(AggressiveState.Heal)
    {
        agroMachine = stateMachine;
    }
    public override void EnterState(){}
    public override void ExitState(){}
    public override void UpdateState(){}
    public override AggressiveState GetNextState(){}
    public override void OnTriggerEnter(Collider other) { }
    public override void OnTriggerStay(Collider other) { }
    public override void OnTriggerExit(Collider other) { }
}
