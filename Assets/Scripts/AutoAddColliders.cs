using UnityEngine;

public class AutoAddColliders : MonoBehaviour
{
    void Start()
    {
        MeshCollider[] existing = GetComponentsInChildren<MeshCollider>();
        if (existing.Length > 0) return;

        MeshFilter[] meshes = GetComponentsInChildren<MeshFilter>();
        foreach (MeshFilter mf in meshes)
        {
            if (mf.GetComponent<MeshCollider>() == null)
            {
                mf.gameObject.AddComponent<MeshCollider>();
            }
        }
    }
}
