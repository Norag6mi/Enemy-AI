using UnityEngine;
using UnityEngine.AI;

public enum AggressiveState
{
    Attack,
    Reload,
    Heal,
    Die
}

public class AgroStateMachine : StateManager<AggressiveState>
{
    public NavMeshAgent Agent; 
    [HideInInspector] public CombatController Combat; //  Reference to the body
    [HideInInspector] public MerceneryStateMachine ParentMachine; // Reference to global data

    void Awake()
    {
        Agent = GetComponent<NavMeshAgent>();
        Combat = GetComponent<CombatController>();
        ParentMachine = GetComponent<MerceneryStateMachine>();

        States.Add(AggressiveState.Attack, new AttackState(this));
        States.Add(AggressiveState.Reload, new ReloadState(this));
        States.Add(AggressiveState.Heal, new HealState(this));
        States.Add(AggressiveState.Die, new DieState(this));

        CurrentState = States[AggressiveState.Attack];
        Combat.OnAmmoEmptyEvent += () => TransitionToState(AggressiveState.Reload);
    }

    
    void Update()
    {
        if (CurrentState == null) return;

        CurrentState.UpdateState();

        AggressiveState nextState = CurrentState.GetNextState();
        if (nextState != CurrentState.StateKey)
        {
            TransitionToState(nextState);
        }
    }
}