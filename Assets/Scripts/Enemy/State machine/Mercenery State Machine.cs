using UnityEngine;
using UnityEngine.AI;

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

    public NavMeshAgent Agent; 
    public Vector3 LastKnownPosition; // For Suspicious state movement
    

    void Awake()
    {
        awareness = GetComponentInChildren<NPCAwareness>();

        if (awareness == null)
        Debug.LogError("NPCAwareness component not found!");

        // 2. Automatically grab the agent if it's on the same object
        Agent = GetComponent<NavMeshAgent>();
        
        States.Add(MerceneryState.Patrol, new PatrolState(this));
        States.Add(MerceneryState.Suspicious, new SuspiciousState(this));
        States.Add(MerceneryState.Agro, new AgroState(this));

        CurrentState = States[MerceneryState.Patrol];
    }

    public MerceneryState EvaluateAwarenessState()
    {
        if (awareness !=null)
        {
            this.LastKnownPosition = awareness.lastKnownPosition;
            
            if (awareness.awareness >= agroThreshold)
                return MerceneryState.Agro;

            if (awareness.awareness >= suspiciousThreshold)
                return MerceneryState.Suspicious;
        }

        return MerceneryState.Patrol;
    }
}