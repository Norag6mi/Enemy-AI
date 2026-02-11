using UnityEngine;

public class SuspiciousState : BaseState<MerceneryState>
{
    MerceneryStateMachine npc;
    GameObject marker;

    public SuspiciousState(MerceneryStateMachine stateMachine)
        : base(MerceneryState.Suspicious)
    {
        npc = stateMachine;
    }

    public override void EnterState()
    {
        marker = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        marker.transform.SetParent(npc.transform);
        marker.transform.localPosition = Vector3.up * 3f;
        marker.transform.localScale = Vector3.one * 0.4f;

        Renderer r = marker.GetComponent<Renderer>();

        r.material = new Material(
            Shader.Find("Universal Render Pipeline/Lit")
        );

        r.material.color = Color.yellow;

        Object.Destroy(marker.GetComponent<Collider>());
    }

    public override void ExitState()
    {
        if (marker != null)
            GameObject.Destroy(marker);
    }

    public override void UpdateState() { }

    public override MerceneryState GetNextState()
    {
        return npc.EvaluateAwarenessState();
    }

    public override void OnTriggerEnter(Collider other) { }
    public override void OnTriggerStay(Collider other) { }
    public override void OnTriggerExit(Collider other) { }
}