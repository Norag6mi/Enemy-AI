using UnityEngine;

public class AttackState : BaseState<AggressiveState>
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private AgroStateMachine agroMachine;

    // This is the constructor
    public AttackState(AgroStateMachine stateMachine) : base(AggressiveState.Attack)
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
