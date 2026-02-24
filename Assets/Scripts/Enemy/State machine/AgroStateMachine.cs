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

    private WeaponComponent weapon;//for weapon component i used in awake
    private HealthComponent health;//for health component i used in awake

    [SerializeField] private int HealStateThreshold = 40;

    void Awake()
    {
        Agent = GetComponent<NavMeshAgent>();
        Combat = GetComponent<CombatController>();
        ParentMachine = GetComponent<MerceneryStateMachine>();

        weapon = GetComponent<WeaponComponent>();//fetching weapon component from child for the ammo count
        health = GetComponent<HealthComponent>();//fetching healthing component from child

        States.Add(AggressiveState.Attack, new AttackState(this));
        States.Add(AggressiveState.Reload, new ReloadState(this));
        States.Add(AggressiveState.Heal, new HealState(this));
        States.Add(AggressiveState.Die, new DieState(this));

        CurrentState = States[AggressiveState.Attack];
        Combat.OnAmmoEmptyEvent += () => TransitionToState(AggressiveState.Reload);

        health.OnDeathEvent += () => TransitionToState(AggressiveState.Die);
    }

    
    void Update()
    {
        if (CurrentState == null || CurrentState.StateKey == AggressiveState.Die) return;
        if (health.Model.CurrentHealth <= HealStateThreshold && CurrentState.StateKey != AggressiveState.Heal)
        {
            TransitionToState(AggressiveState.Heal);
        }

        CurrentState.UpdateState();

        AggressiveState nextState = CurrentState.GetNextState();
        if (nextState != CurrentState.StateKey)
        {
            TransitionToState(nextState);
        }
    }
}