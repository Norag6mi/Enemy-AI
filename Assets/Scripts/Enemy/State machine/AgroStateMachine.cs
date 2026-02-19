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
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        Agent = GetComponent<NavMeshAgent>();

        States.Add(AggressiveState.Attack, new AttackState(this));
        States.Add(AggressiveState.Reload, new ReloadState(this));
        States.Add(AggressiveState.Heal, new HealState(this));
        States.Add(AggressiveState.Die, new DieState(this));

        CurrentState = States[AggressiveState.Attack];
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

