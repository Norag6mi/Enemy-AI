using UnityEngine;

public class NPCVision : MonoBehaviour
{
    [Header("Vision Settings")]
    public float viewRadius = 10f;

    [Range(0, 360)]
    public float viewAngle = 90f;

    [Header("Eye Settings")]
    public float eyeHeight = 1.6f;   // Height of NPC eyes

    [Header("Layers")]
    public LayerMask targetMask;     // Player
    public LayerMask obstacleMask;   // Walls

    // 🔹 Reference to Awareness system
    NPCAwareness awareness;

    void Start()
    {
    awareness = GetComponentInParent<NPCAwareness>();

    if (awareness == null)
        Debug.LogError("NPCAwareness NOT FOUND on NPC!");
    else
        Debug.Log("NPCAwareness connected successfully");
}
    void Update()
    {
        DetectPlayer();
    }

    void DetectPlayer()
    {
        bool canSeeTarget = false;

        // 1. Find all targets in vision radius
        Collider[] targetsInRadius = Physics.OverlapSphere(
            transform.position,
            viewRadius,
            targetMask
        );

        foreach (Collider target in targetsInRadius)
        {
            // 2. Eye position
            Vector3 eyePosition = transform.position + Vector3.up * eyeHeight;

            // 3. Direction from eyes to target
            Vector3 directionToTarget =
                (target.transform.position - eyePosition).normalized;

            // 4. Angle check
            float angleToTarget =
                Vector3.Angle(transform.forward, directionToTarget);

            if (angleToTarget <= viewAngle / 2f)
            {
                float distanceToTarget =
                    Vector3.Distance(eyePosition, target.transform.position);

                // 5. Line of sight check
                if (!Physics.Raycast(
                    eyePosition,
                    directionToTarget,
                    distanceToTarget,
                    obstacleMask))
                {
                    // 👁️ Vision success → increase awareness
                    canSeeTarget = true;

                    if (awareness != null)
                    {
                        awareness.IncreaseAwareness(distanceToTarget);
                    }

                    Debug.DrawRay(
                        eyePosition,
                        directionToTarget * distanceToTarget,
                        Color.red
                    );
                }
            }
        }

        // 👁️ Vision failed → decrease awareness
        if (!canSeeTarget && awareness != null)
        {
            awareness.DecreaseAwareness();
        }
    }

    // Visual helpers (Editor only)
   void OnDrawGizmosSelected()
{
    // ===== Vision Radius =====
    Gizmos.color = Color.yellow;
    Gizmos.DrawWireSphere(transform.position, viewRadius);

    // ===== Eye Position =====
    Vector3 eyePosition = transform.position + Vector3.up * eyeHeight;
    Gizmos.color = Color.cyan;
    Gizmos.DrawSphere(eyePosition, 0.1f);

    // ===== Vision Angle (Cone) =====
    Vector3 forward = transform.forward;

    Vector3 leftBoundary = Quaternion.Euler(0, -viewAngle / 2f, 0) * forward;
    Vector3 rightBoundary = Quaternion.Euler(0, viewAngle / 2f, 0) * forward;

    Gizmos.color = Color.red;
    Gizmos.DrawLine(eyePosition, eyePosition + leftBoundary * viewRadius);
    Gizmos.DrawLine(eyePosition, eyePosition + rightBoundary * viewRadius);

    // Optional: draw forward direction
    Gizmos.color = Color.blue;
    Gizmos.DrawLine(eyePosition, eyePosition + forward * viewRadius);
}

}
