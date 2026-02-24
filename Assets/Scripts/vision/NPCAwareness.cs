using UnityEngine;

public class NPCAwareness : MonoBehaviour
{
    [Header("Awareness Value")]
    [Range(0f, 100f)]
    public float awareness = 0f;

    public float maxAwareness = 100f;

    [Header("Distance Ranges")]
    public float closeRange = 3f;
    public float mediumRange = 7f;
    public float farRange = 12f;

    [Header("Increase Rates (per second)")]
    public float closeIncrease = 40f;
    public float mediumIncrease = 20f;
    public float farIncrease = 8f;

    [Header("Decrease Rates (per second)")]
    public float closeDecrease = 30f;
    public float mediumDecrease = 15f;
    public float farDecrease = 5f;

    float lastSeenDistance = Mathf.Infinity;


     // ... existing variables ...
    
    [HideInInspector] 
    public Vector3 lastKnownPosition; //  Store the world position here
    [HideInInspector] public Transform currentTarget;//I am storing the actual object here


    public void IncreaseAwareness(float distance, Vector3 targetPos) // 🔹 Added parameter
    {
        lastSeenDistance = distance;
        lastKnownPosition = targetPos; // 🔹 Save the position!

        float rate = GetIncreaseRate(distance);
        awareness += rate * Time.deltaTime;
        awareness = Mathf.Clamp(awareness, 0f, maxAwareness);

        Debug.Log($"👁️ AWARENESS ↑ | {awareness:F1} | Distance: {distance:F1}");
    }

    public void IncreaseAwareness(float distance, Transform target) // 🔹 Added parameter
    {
        currentTarget = target;
        lastKnownPosition = target.position; // 🔹 Save the position!

        float rate = GetIncreaseRate(distance);
        awareness += rate * Time.deltaTime;
        awareness = Mathf.Clamp(awareness, 0f, maxAwareness);

        Debug.Log($"👁️ AWARENESS ↑ | {awareness:F1} | Distance: {distance:F1}");
    }

    public void DecreaseAwareness()
    {
        float rate = GetDecreaseRate(lastSeenDistance);
        awareness -= rate * Time.deltaTime;

        awareness = Mathf.Clamp(awareness, 0f, maxAwareness);

        Debug.Log($"👁️ AWARENESS ↓ | {awareness:F1}");
    }

    float GetIncreaseRate(float distance)
    {
        if (distance <= closeRange) return closeIncrease;
        if (distance <= mediumRange) return mediumIncrease;
        return farIncrease;
    }

    float GetDecreaseRate(float distance)
    {
        if (distance <= closeRange) return closeDecrease;
        if (distance <= mediumRange) return mediumDecrease;
        return farDecrease;
    }
}