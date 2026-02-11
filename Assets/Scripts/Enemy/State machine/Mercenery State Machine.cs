using UnityEngine;

public enum MerceneryState
{
    Patrol,
    Suspicious,
    Agro
}

public class MerceneryStateMachine : StateManager<MerceneryState>
{
    public float suspiciousThreshold = 30f;
    public float agroThreshold = 70f;
    

    void Awake()
    {
        awareness = GetComponentInChildren<NPCAwareness>();

        if (awareness == null)
        Debug.LogError("NPCAwareness component not found!");
        
        States.Add(MerceneryState.Patrol, new PatrolState(this));
        States.Add(MerceneryState.Suspicious, new SuspiciousState(this));
        States.Add(MerceneryState.Agro, new AgroState(this));

        CurrentState = States[MerceneryState.Patrol];
    }

    public MerceneryState EvaluateAwarenessState()
    {
        if (awareness.awareness >= agroThreshold)
            return MerceneryState.Agro;

        if (awareness.awareness >= suspiciousThreshold)
            return MerceneryState.Suspicious;

        return MerceneryState.Patrol;
    }
}