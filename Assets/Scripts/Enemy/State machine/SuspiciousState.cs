using UnityEngine;
using Game.NavigationTutorial;

public class SuspiciousState : BaseState<MerceneryState>
{
    private MerceneryStateMachine npc;
    private GameObject marker;
    private NPCWander wanderLogic;

    public SuspiciousState(MerceneryStateMachine stateMachine)
        : base(MerceneryState.Suspicious)
    {
        npc = stateMachine;
        wanderLogic = npc.GetComponent<NPCWander>();
    }

    public override void EnterState()
    {
        StopWanderBehavior();
        StartInvestigation();
        CreateVisualIndicator();
    }

    public override void UpdateState()
    {
        RotateTowardsTarget();
    }

    public override void ExitState()
    {
        CleanupVisuals();
    }

    // --- Core Logic Functions ---

    private void StopWanderBehavior()
    {
        // Disabling this triggers OnDisable() in NPCWander, cleaning up the Agent
        if (wanderLogic != null)
        {
            wanderLogic.enabled = false;
        }
    }

    private void StartInvestigation()
    {
        if (npc.Agent != null)
        {
            // Ensure Agent is active (just in case)
            npc.Agent.enabled = true;

            if (npc.Agent.isOnNavMesh)
            {
                npc.Agent.ResetPath();
                npc.Agent.SetDestination(npc.LastKnownPosition);
                npc.Agent.speed = 2.0f; // Slower, suspicious walk speed
            }
        }
    }

    private void RotateTowardsTarget()
    {
        Vector3 direction = (npc.LastKnownPosition - npc.transform.position).normalized;
        direction.y = 0; // Prevent the NPC from tilting up/down

        if (direction != Vector3.zero)
        {
            Quaternion targetRot = Quaternion.LookRotation(direction);
            npc.transform.rotation = Quaternion.Slerp(npc.transform.rotation, targetRot, Time.deltaTime * 5f);
        }
    }

    // --- Visuals ---

    private void CreateVisualIndicator()
    {
        marker = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        marker.transform.SetParent(npc.transform);
        marker.transform.localPosition = Vector3.up * 3f;
        marker.transform.localScale = Vector3.one * 0.4f;

        Renderer r = marker.GetComponent<Renderer>();
        r.material = new Material(Shader.Find("Universal Render Pipeline/Lit"));
        r.material.color = Color.yellow;

        Object.Destroy(marker.GetComponent<Collider>());
    }

    private void CleanupVisuals()
    {
        if (marker != null) Object.Destroy(marker);
    }

    public override MerceneryState GetNextState() => npc.EvaluateAwarenessState();

    public override void OnTriggerEnter(Collider other) { }
    public override void OnTriggerStay(Collider other) { }
    public override void OnTriggerExit(Collider other) { }
}